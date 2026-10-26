using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace Turist.Data;

public class TuristDbContextFactory : IDesignTimeDbContextFactory<TuristDbContext>
{
    public TuristDbContext CreateDbContext(string[] args)
    {
        // 1. Buscamos el archivo .env en la carpeta de la API.
        // La ruta sube un nivel desde Turist.Data y entra a Turist.Api.
        Env.Load("../Turist.Api/.env"); 

        // 2. Construimos la cadena de conexión con las variables cargadas
        var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                               $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                               $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                               $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                               $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

        var optionsBuilder = new DbContextOptionsBuilder<TuristDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TuristDbContext(optionsBuilder.Options);
    }
}