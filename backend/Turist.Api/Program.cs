using Microsoft.EntityFrameworkCore;
using Turist.Data;
using DotNetEnv;
using Turist.Data.Repositories;
using Turist.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.TraversePath().Load();

// 1. Configurar la Base de Datos (Capa de Datos)
var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                       ?? $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

builder.Services.AddDbContext<TuristDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. REGISTRO DE REPOSITORIOS (Inyección de Dependencias)
builder.Services.AddScoped<IParadaRepository, ParadaRepository>(); // <-- AQUÍ ESTÁ TU LÍNEA

// 3. Configurar Servicios de la API (Controladores y OpenAPI)
builder.Services.AddControllers();
builder.Services.AddOpenApi();

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