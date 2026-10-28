using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace Turist.Data;

public class TuristDbContextFactory : IDesignTimeDbContextFactory<TuristDbContext>
{
    public TuristDbContext CreateDbContext(string[] args)
    {
        // 1. Cargar el archivo .env (Ajusta la ruta si es necesario)
        // Como estamos en Turist.Data, el .env está en la carpeta de la API
        Env.Load("../Turist.Api/.env"); 

        // 2. Construir la cadena de conexión
        var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                               ?? $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

        var optionsBuilder = new DbContextOptionsBuilder<TuristDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TuristDbContext(optionsBuilder.Options);
    }
}