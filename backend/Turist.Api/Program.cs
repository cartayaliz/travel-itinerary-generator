using Microsoft.EntityFrameworkCore;
using Turist.Data;
using Turist.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la Base de Datos (Capa de Datos)
builder.Services.AddDbContext<TuristDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Configurar Servicios de la API (Controladores y OpenAPI)
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // El nuevo estándar de .NET 9/10
builder.Services.AddScoped<ITouristRepository, TouristRepository>();
var app = builder.Build();

// 3. Configurar el pipeline de peticiones HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Mapea la UI de OpenAPI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 

app.Run();
