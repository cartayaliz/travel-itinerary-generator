using Microsoft.EntityFrameworkCore;
using Turist.Data;
using Turist.Data.Repositories;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services;
using DotNetEnv;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Turist.Services.Interfaces;
using Turist.Domain.Interfaces.Services;
using Turist.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar el archivo .env
DotNetEnv.Env.Load("../../.env");

// 2. Configurar la Base de Datos
var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                       ?? $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

builder.Services.AddDbContext<TuristDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. Configurar la Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? Environment.GetEnvironmentVariable("JWT_KEY")!))
        };
    });

builder.Services.AddAuthorization();

// 4. Registrar Repositorios (Necesarios para que los Servicios)
builder.Services.AddScoped<IParadaRepository, ParadaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPaqueteTuristicoRepository, PaqueteTuristicoRepository>();
builder.Services.AddScoped<IItinerarioRepository, ItinerarioRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IVisitaRepository, VisitaRepository>();
builder.Services.AddScoped<IModificacionRepository, ModificacionRepository>();
builder.Services.AddScoped<IValidacionRepository, ValidacionRepository>();
builder.Services.AddScoped<IValoracionRepository, ValoracionRepository>();
builder.Services.AddScoped<ITuristaRepository, TuristaRepository>();
builder.Services.AddScoped<IGuiaRepository, GuiaRepository>();

// 5. Registrar Servicios
builder.Services.AddScoped<IParadaService, ParadaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IGrupoService, GrupoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITuristaService, TuristaService>();
builder.Services.AddScoped<IGuiaService, GuiaService>();
builder.Services.AddScoped<IJefeService, JefeService>();
builder.Services.AddScoped<IPaqueteTuristicoService, PaqueteTuristicoService>();
builder.Services.AddScoped<IItinerarioService, ItinerarioService>();
builder.Services.AddScoped<IVisitaService, VisitaService>();
builder.Services.AddScoped<IModificacionService, ModificacionService>();
builder.Services.AddScoped<IValidacionService, ValidacionService>();
builder.Services.AddScoped<IValoracionService, ValoracionService>();
builder.Services.AddScoped<IItineraryGenerationStrategy, CriteriosItineraryGenerationStrategy>();
builder.Services.AddScoped<IItinerarioService, ItinerarioService>();
builder.Services.AddScoped<IParadaService, ParadaService>();

// 5. Configurar Controladores y OpenAPI

// 6. Configurar Controladores y OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// 7. Pipeline de peticiones HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();