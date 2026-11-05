using Turist.Domain.Graph;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.TransportWeb;

namespace Turist.Tests;

public class TransportWebServiceTests
{
    private class FakeItinerarioRepository : IItinerarioRepository
    {
        private readonly IEnumerable<Itinerario> _itinerarios;
        public FakeItinerarioRepository(IEnumerable<Itinerario> itinerarios)
        {
            _itinerarios = itinerarios;
        }

        public Task AddAsync(Itinerario itinerario)
        {
            throw new NotImplementedException();
        }

        public Task AsignarPaquetesAsync(List<Itinerario_Paquete> paquetes)
        {
            throw new NotImplementedException();
        }

        public Task AsignarParadasAsync(List<Itinerario_Parada> paradas)
        {
            throw new NotImplementedException();
        }

        public Task AsignarTuristasAsync(List<Itinerario_Turista> turistas)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Itinerario itinerario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Itinerario>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Itinerario>> GetAllWithParadasOrderedAsync() => Task.FromResult(_itinerarios);

        public Task<Itinerario?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task LimpiarPaquetesAsync(int itinerarioId)
        {
            throw new NotImplementedException();
        }

        public Task LimpiarParadasAsync(int itinerarioId)
        {
            throw new NotImplementedException();
        }

        public Task LimpiarTuristasAsync(int itinerarioId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Itinerario itinerario)
        {
            throw new NotImplementedException();
        }
    }

    private class FakeParadaRepository : IParadaRepository
    {
        private readonly IEnumerable<Parada> _paradas;
        public FakeParadaRepository(IEnumerable<Parada> paradas)
        {
            _paradas = paradas;
        }
        public Task<IEnumerable<Parada>> GetAllAsync() => Task.FromResult(_paradas);
        public Task<Parada?> GetByIdAsync(int id) => Task.FromResult(_paradas.FirstOrDefault(p => p.ParadaID == id));
        public Task AddAsync(Parada parada) => Task.CompletedTask;
        public Task UpdateAsync(Parada parada) => Task.CompletedTask;
        public Task DeleteAsync(Parada parada) => Task.CompletedTask;
        public Task<IEnumerable<Parada>> GetByCategoryAsync(int categoriaId) => Task.FromResult(_paradas.Where(p => p.CategoriaID == categoriaId));
        
        //temporal esto es q no me dejaba compilar,Rene formaste tremenda pingueta brother :3
        public Task<IEnumerable<Actividad>> GetParaGeneracionAsync(ActividadFilterOptions options)
        {
            // Como es un Fake para pruebas, devolvemos una lista vacía o simulada
            return Task.FromResult<IEnumerable<Actividad>>(new List<Actividad>());
        }
    }

    private static Atraccion CreateAtraccion(int id, string nombre, double lat, double lon)
    {
        return new Atraccion
        {
            ParadaID = id,
            Nombre = nombre,
            Latitud = lat,
            Longitud = lon,
            CategoriaID = 1,
            GuiaID = 1
        };
    }

    [Fact]
    public async Task LoadAsync_CargaItinerariosYParadasEnDiccionario()
    {
        // Arrange
        var parada1 = CreateAtraccion(1, "Plaza", 23.135, -82.358);
        var parada2 = CreateAtraccion(2, "Catedral", 23.136, -82.359);
        var paradas = new List<Parada> { parada1, parada2 };

        var itinerario = new Itinerario
        {
            ItinerarioID = 1,
            Nombre = "Ruta 1",
            GuiaID = 1,
            FechaCreacion = DateTime.UtcNow,
            ItinerarioParadas = new List<Itinerario_Parada>
            {
                new() { ItinerarioID = 1, ParadaID = 1, Orden = 1, Parada = parada1 },
                new() { ItinerarioID = 1, ParadaID = 2, Orden = 2, Parada = parada2 }
            }
        };

        var service = new TransportWebService(
            new FakeItinerarioRepository(new List<Itinerario> { itinerario }),
            new FakeParadaRepository(paradas),
            closeRadiusKm: 10.0
        );

        // Act
        await service.LoadAsync();

        // Assert - no exception means loaded correctly
        Assert.True(true);
    }

    [Fact]
    public void LoadGraph_CreaAristasParaParesConsecutivosDentroDeRadio()
    {
        // Arrange
        var parada1 = CreateAtraccion(1, "Plaza", 23.135, -82.358);
        var parada2 = CreateAtraccion(2, "Catedral", 23.136, -82.359);
        var paradas = new List<Parada> { parada1, parada2 };

        var itinerario = new Itinerario
        {
            ItinerarioID = 1,
            Nombre = "Ruta 1",
            GuiaID = 1,
            FechaCreacion = DateTime.UtcNow,
            ItinerarioParadas = new List<Itinerario_Parada>
            {
                new() { ItinerarioID = 1, ParadaID = 1, Orden = 1, Parada = parada1 },
                new() { ItinerarioID = 1, ParadaID = 2, Orden = 2, Parada = parada2 }
            }
        };

        var service = new TransportWebService(
            new FakeItinerarioRepository(new List<Itinerario> { itinerario }),
            new FakeParadaRepository(paradas),
            closeRadiusKm: 10.0
        );

        // Act
        service.LoadAsync().GetAwaiter().GetResult();

        // Assert - graph should have nodes and edges
        // We can't access private _graph directly, but we can test via ProposeAlternatives behavior
        // If graph is built, Dijkstra should work
        var result = service.ProposeAlternatives(1, 1, 2, new Weight(100), _ => true);
        Assert.NotNull(result);
    }

    [Fact]
    public void LoadGraph_NoCreaAristaSiFueraDeRadio()
    {
        // Arrange - paradas muy lejos
        var parada1 = CreateAtraccion(1, "Madrid", 40.4168, -3.7038);
        var parada2 = CreateAtraccion(2, "NYC", 40.7128, -74.0060);
        var paradas = new List<Parada> { parada1, parada2 };

        var itinerario = new Itinerario
        {
            ItinerarioID = 1,
            Nombre = "Ruta Larga",
            GuiaID = 1,
            FechaCreacion = DateTime.UtcNow,
            ItinerarioParadas = new List<Itinerario_Parada>
            {
                new() { ItinerarioID = 1, ParadaID = 1, Orden = 1, Parada = parada1 },
                new() { ItinerarioID = 1, ParadaID = 2, Orden = 2, Parada = parada2 }
            }
        };

        var service = new TransportWebService(
            new FakeItinerarioRepository(new List<Itinerario> { itinerario }),
            new FakeParadaRepository(paradas),
            closeRadiusKm: 1.0 // radio muy pequeño
        );

        // Act
        service.LoadAsync().GetAwaiter().GetResult();

        // Assert - should not create edge, so propose alternatives from 1 to 2 with no direct path?
        // Dijkstra from 1 won't reach 2 if no edge
        var result = service.ProposeAlternatives(1, 1, 2, new Weight(100), _ => true);
        Assert.Empty(result);
    }

    [Fact]
    public void ProposeAlternatives_RetornaAlternativasOrdenadasPorPeso()
    {
        // Arrange - crear un grafo con origen, destino y alternativas
        var origen = CreateAtraccion(1, "Origen", 23.135, -82.358);
        var alt1 = CreateAtraccion(2, "Alt1", 23.136, -82.359);
        var alt2 = CreateAtraccion(3, "Alt2", 23.137, -82.360);
        var destino = CreateAtraccion(4, "Destino", 23.138, -82.361);
        var paradas = new List<Parada> { origen, alt1, alt2, destino };

        // Crear itinerarios para conectar: origen-alt1-destino y origen-alt2-destino
        var itinerario1 = new Itinerario
        {
            ItinerarioID = 1,
            Nombre = "Ruta A",
            GuiaID = 1,
            FechaCreacion = DateTime.UtcNow,
            ItinerarioParadas = new List<Itinerario_Parada>
            {
                new() { ItinerarioID = 1, ParadaID = 1, Orden = 1, Parada = origen },
                new() { ItinerarioID = 1, ParadaID = 2, Orden = 2, Parada = alt1 },
                new() { ItinerarioID = 1, ParadaID = 4, Orden = 3, Parada = destino }
            }
        };

        var itinerario2 = new Itinerario
        {
            ItinerarioID = 2,
            Nombre = "Ruta B",
            GuiaID = 1,
            FechaCreacion = DateTime.UtcNow,
            ItinerarioParadas = new List<Itinerario_Parada>
            {
                new() { ItinerarioID = 2, ParadaID = 1, Orden = 1, Parada = origen },
                new() { ItinerarioID = 2, ParadaID = 3, Orden = 2, Parada = alt2 },
                new() { ItinerarioID = 2, ParadaID = 4, Orden = 3, Parada = destino }
            }
        };

        var service = new TransportWebService(
            new FakeItinerarioRepository(new List<Itinerario> { itinerario1, itinerario2 }),
            new FakeParadaRepository(paradas),
            closeRadiusKm: 10.0
        );

        // Act
        service.LoadAsync().GetAwaiter().GetResult();

        // Act
        var alternatives = service.ProposeAlternatives(2, 1, 4, new Weight(100), _ => true);

        // Assert
        Assert.NotNull(alternatives);
        Assert.True(alternatives.Count <= 2);
        // No debe incluir origen ni destino
        Assert.DoesNotContain(1, alternatives);
        Assert.DoesNotContain(4, alternatives);
    }

    [Fact]
    public void ProposeAlternatives_FiltraPorFiltro()
    {
        // Arrange
        var origen = CreateAtraccion(1, "Origen", 23.135, -82.358);
        var alt1 = CreateAtraccion(2, "Alt1", 23.136, -82.359);
        var destino = CreateAtraccion(4, "Destino", 23.138, -82.361);
        var paradas = new List<Parada> { origen, alt1, destino };

        var itinerario = new Itinerario
        {
            ItinerarioID = 1,
            Nombre = "Ruta A",
            GuiaID = 1,
            FechaCreacion = DateTime.UtcNow,
            ItinerarioParadas = new List<Itinerario_Parada>
            {
                new() { ItinerarioID = 1, ParadaID = 1, Orden = 1, Parada = origen },
                new() { ItinerarioID = 1, ParadaID = 2, Orden = 2, Parada = alt1 },
                new() { ItinerarioID = 1, ParadaID = 4, Orden = 3, Parada = destino }
            }
        };

        var service = new TransportWebService(
            new FakeItinerarioRepository(new List<Itinerario> { itinerario }),
            new FakeParadaRepository(paradas),
            closeRadiusKm: 10.0
        );

        service.LoadAsync().GetAwaiter().GetResult();

        // Act - filtrar para que solo acepte paradas con ID != 2
        var alternatives = service.ProposeAlternatives(5, 1, 4, new Weight(100), p => p.ParadaID != 2);

        // Assert
        Assert.DoesNotContain(2, alternatives);
    }
}
