using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace Turist.Data;

public class TuristDbContextFactory : IDesignTimeDbContextFactory<TuristDbContext>
{
    public TuristDbContext CreateDbContext(string[] args)
    {

        // 1. Cargar el archivo .env para que las herramientas de EF Core lo detecten
        DotNetEnv.Env.TraversePath().Load();

        // 2. Construir la cadena de conexión usando las variables de tu .env
        var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION") 
                               ?? $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

        var optionsBuilder = new DbContextOptionsBuilder<TuristDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TuristDbContext(optionsBuilder.Options);
    }
}