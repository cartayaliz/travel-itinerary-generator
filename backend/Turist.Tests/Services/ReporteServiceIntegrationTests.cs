using Microsoft.EntityFrameworkCore;
using Turist.Data;
using Turist.Data.Repositories;
using Turist.Domain.Enums;
using Turist.Domain.Models;
using Turist.Services.Services;
using Xunit;

namespace Turist.Tests.Services;
//para testear 
//dotnet test --filter "FullyQualifiedName~ReporteServiceIntegrationTests"
public class ReporteServiceIntegrationTests
{
    // Este método crea un contexto en memoria con un "Universo" de datos completo
    private async Task<TuristDbContext> GetDbContextWithComplexDataAsync()
    {
        var options = new DbContextOptionsBuilder<TuristDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new TuristDbContext(options);

        // 1. Categorías
        var catCultural = new Categoria { CategoriaID = 1, Nombre = "Cultural" };
        var catAventura = new Categoria { CategoriaID = 2, Nombre = "Aventura" };
        context.Categorias.AddRange(catCultural, catAventura);

        // 2. Usuarios (Guía y Turista)
        var guia = new GuiaTuristico { UsuarioID = 1, Nombre = "Guía 1" };
        var turista = new Turista { UsuarioID = 2, Nombre = "Turista 1", Edad = 25, GrupoID = 1 };
        context.Usuarios.AddRange(guia, turista);

        // 3. Paradas
        var act1 = new Actividad { ParadaID = 1, Nombre = "Tour Catedral", CategoriaID = 1, GuiaID = 1, NivelEsfuerzo = NivelEsfuerzo.Medio, Categoria = catCultural, Guia = guia };
        var act2 = new Actividad { ParadaID = 2, Nombre = "Escalada", CategoriaID = 2, GuiaID = 1, NivelEsfuerzo = NivelEsfuerzo.Alto, Categoria = catAventura, Guia = guia };
        var atraccion = new Atraccion { ParadaID = 3, Nombre = "Estatua", CategoriaID = 1, GuiaID = 1, Categoria = catCultural, Guia = guia };
        context.Paradas.AddRange(act1, act2, atraccion);

        // 4. Paquetes e Itinerarios
        var paquete1 = new PaqueteTuristico { PaqueteID = 1, Nombre = "Pack 1", Destino = "La Habana" };
        var paquete2 = new PaqueteTuristico { PaqueteID = 2, Nombre = "Pack 2", Destino = "Viñales" };
        context.PaquetesTuristicos.AddRange(paquete1, paquete2);

        var itin1 = new Itinerario { ItinerarioID = 1, Nombre = "Ruta 1", GuiaID = 1, Guia = guia };
        var itin2 = new Itinerario { ItinerarioID = 2, Nombre = "Ruta 2", GuiaID = 1, Guia = guia };
        context.Itinerarios.AddRange(itin1, itin2);

        // 5. Relaciones Itinerario - Paquete
        context.Itinerario_Paquetes.AddRange(
            new Itinerario_Paquete { ItinerarioID = 1, PaqueteID = 1, Itinerario = itin1, Paquete = paquete1 },
            new Itinerario_Paquete { ItinerarioID = 2, PaqueteID = 1, Itinerario = itin2, Paquete = paquete1 },
            new Itinerario_Paquete { ItinerarioID = 2, PaqueteID = 2, Itinerario = itin2, Paquete = paquete2 }
        );

        // 6. Relaciones Itinerario - Parada
        context.Itinerario_Paradas.AddRange(
            new Itinerario_Parada { ItinerarioID = 1, ParadaID = 1, Orden = 1, Itinerario = itin1, Parada = act1 },
            new Itinerario_Parada { ItinerarioID = 1, ParadaID = 2, Orden = 2, Itinerario = itin1, Parada = act2 },
            new Itinerario_Parada { ItinerarioID = 2, ParadaID = 1, Orden = 1, Itinerario = itin2, Parada = act1 },
            new Itinerario_Parada { ItinerarioID = 2, ParadaID = 3, Orden = 2, Itinerario = itin2, Parada = atraccion }
        );

        // 7. Visitas (Dos visitas diferentes para calcular promedios)
        var visita1 = new Visita { VisitaID = 1, ItinerarioID = 1, TuristaID = 2, ParadaID = 1, Estado = EstadoVisita.Completada, Turista = turista, Parada = act1 };
        var visita2 = new Visita { VisitaID = 2, ItinerarioID = 1, TuristaID = 2, ParadaID = 2, Estado = EstadoVisita.Completada, Turista = turista, Parada = act2 };
        context.Visitas.AddRange(visita1, visita2);

        // 8. Valoraciones (Una por visita)
        context.Valoraciones.AddRange(
            new Valoracion { ValoracionID = 1, Puntuacion = 5, TuristaID = 2, VisitaID = 1, Visita = visita1 },
            new Valoracion { ValoracionID = 2, Puntuacion = 3, TuristaID = 2, VisitaID = 2, Visita = visita2 }
        );

        await context.SaveChangesAsync();
        return context;
    }

    [Fact]
    public async Task GetActividadesMasUsadasAsync_DeberiaContarSoloActividadesEIgnorarAtracciones()
    {
        var context = await GetDbContextWithComplexDataAsync();
        var service = new ReporteService(new ReporteRepository(context));

        var result = await service.GetActividadesMasUsadasAsync();

        Assert.Equal(2, result.Count());
        var tourCatedral = result.FirstOrDefault(r => r.NombreActividad == "Tour Catedral");
        Assert.NotNull(tourCatedral);
        Assert.Equal(2, tourCatedral.CantidadUsos);

        var escalada = result.FirstOrDefault(r => r.NombreActividad == "Escalada");
        Assert.NotNull(escalada);
        Assert.Equal(1, escalada.CantidadUsos);
        Assert.DoesNotContain(result, r => r.NombreActividad == "Estatua");
    }

    [Fact]
    public async Task GetSatisfaccionPorEsfuerzoAsync_DeberiaCalcularPromediosCorrectamente()
    {
        var context = await GetDbContextWithComplexDataAsync();
        var service = new ReporteService(new ReporteRepository(context));

        var result = await service.GetSatisfaccionPorEsfuerzoAsync();

        Assert.Equal(2, result.Count());

        var medio = result.FirstOrDefault(r => r.NivelEsfuerzo == "Medio");
        Assert.NotNull(medio);
        Assert.Equal(5.0, medio.PromedioValoracion);
        Assert.Equal(1, medio.TotalValoraciones);

        var alto = result.FirstOrDefault(r => r.NivelEsfuerzo == "Alto");
        Assert.NotNull(alto);
        Assert.Equal(3.0, alto.PromedioValoracion);
        Assert.Equal(1, alto.TotalValoraciones);
    }

    [Fact]
    public async Task GetComparacionPaquetesAsync_DeberiaAgruparPorPaqueteYCalcularPromedios()
    {
        var context = await GetDbContextWithComplexDataAsync();
        var service = new ReporteService(new ReporteRepository(context));

        var result = await service.GetComparacionPaquetesAsync();

        Assert.Equal(2, result.Count());

        var pack1 = result.FirstOrDefault(r => r.NombrePaquete == "Pack 1");
        Assert.NotNull(pack1);
        Assert.Equal(2, pack1.TotalItinerarios);
        Assert.Equal(4, pack1.TotalParadas);
        Assert.Equal(2.33, Math.Round(pack1.PromedioEsfuerzo, 2));

        var pack2 = result.FirstOrDefault(r => r.NombrePaquete == "Pack 2");
        Assert.NotNull(pack2);
        Assert.Equal(1, pack2.TotalItinerarios);
        Assert.Equal(2, pack2.TotalParadas);
        Assert.Equal(2.0, pack2.PromedioEsfuerzo);
    }
    [Fact]
    public async Task GetActividadesMasUsadasAsync_DeberiaIgnorarActividadesQueNoEstanEnNingunItinerario()
    {
        // Arrange
        var context = await GetDbContextWithComplexDataAsync();

        // Creamos una actividad nueva, pero NO la metemos en ningún itinerario
        var actividadHuerfana = new Actividad
        {
            ParadaID = 99,
            Nombre = "Actividad Fantasma",
            CategoriaID = 1,
            GuiaID = 1,
            NivelEsfuerzo = NivelEsfuerzo.Bajo
        };
        context.Paradas.Add(actividadHuerfana);
        await context.SaveChangesAsync();

        var service = new ReporteService(new ReporteRepository(context));

        // Act
        var result = await service.GetActividadesMasUsadasAsync();

        // Assert
        // El reporte solo debe mostrar 2 actividades (las que están en itinerarios).
        // La "Actividad Fantasma" NO debe aparecer porque su uso es 0.
        Assert.Equal(2, result.Count());
        Assert.DoesNotContain(result, r => r.NombreActividad == "Actividad Fantasma");
    }

    [Fact]
    public async Task GetSatisfaccionPorEsfuerzoAsync_DeberiaManejarActividadesSinValoraciones()
    {
        // Arrange
        var context = await GetDbContextWithComplexDataAsync();

        // Agregamos una Visita para la actividad "Tour Catedral", pero SIN VALORACIÓN
        var visitaSinValoracion = new Visita
        {
            VisitaID = 3,
            ItinerarioID = 1,
            TuristaID = 2,
            ParadaID = 1,
            Estado = EstadoVisita.Completada
        };
        context.Visitas.Add(visitaSinValoracion);
        await context.SaveChangesAsync();

        var service = new ReporteService(new ReporteRepository(context));

        // Act
        var result = await service.GetSatisfaccionPorEsfuerzoAsync();

        // Assert
        // El reporte debe seguir funcionando. 
        // El promedio de "Medio" debe seguir siendo 5.0 (ignorando la visita sin valoración).
        Assert.Equal(2, result.Count());
        var medio = result.FirstOrDefault(r => r.NivelEsfuerzo == "Medio");
        Assert.NotNull(medio);
        Assert.Equal(5.0, medio.PromedioValoracion);
        Assert.Equal(1, medio.TotalValoraciones); // Sigue siendo 1, no 2
    }

    [Fact]
    public async Task GetComparacionPaquetesAsync_DeberiaManejarPaquetesSinItinerarios()
    {
        // Arrange
        var context = await GetDbContextWithComplexDataAsync();

        // Creamos un paquete vacío (sin itinerarios)
        var paqueteVacio = new PaqueteTuristico { PaqueteID = 3, Nombre = "Pack Vacío", Destino = "Ninguno" };
        context.PaquetesTuristicos.Add(paqueteVacio);
        await context.SaveChangesAsync();

        var service = new ReporteService(new ReporteRepository(context));

        // Act
        var result = await service.GetComparacionPaquetesAsync();

        // Assert
        // Debe haber 3 paquetes en total
        Assert.Equal(3, result.Count());

        var packVacio = result.FirstOrDefault(r => r.NombrePaquete == "Pack Vacío");
        Assert.NotNull(packVacio);
        Assert.Equal(0, packVacio.TotalItinerarios);
        Assert.Equal(0, packVacio.TotalParadas);
        // ¡AQUÍ ESTÁ EL TRUCO! El promedio de esfuerzo debe ser 0 (no debe dar error de división por cero)
        Assert.Equal(0, packVacio.PromedioEsfuerzo);
    }
}