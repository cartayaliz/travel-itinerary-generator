using Microsoft.EntityFrameworkCore;
using Turist.Data;
using Turist.Domain.Repository;
using DotNetEnv;
using Turist.Domain.Models;
using Turist.Api.Infrastructure;
using Turist.Data.Repositories;
using Turist.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);
// 1. Cargar el archivo .env para que las herramientas de EF Core lo detecten
        DotNetEnv.Env.TraversePath().Load();

        // 2. Construir la cadena de conexión usando las variables de tu .env
        var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION") 
                               ?? $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

builder.Services.AddDbContext<TuristDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. REGISTRO DE REPOSITORIOS (Inyección de Dependencias)
builder.Services.AddScoped<IParadaRepository, ParadaRepository>(); // <-- AQUÍ ESTÁ TU LÍNEA

// 3. Configurar Servicios de la API (Controladores y OpenAPI)
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // El nuevo estándar de .NET 9/10
builder.Services.AddScoped<IRepository<Usuario>, Repository>();
var app = builder.Build();

// 4. Configurar el pipeline de peticiones HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();