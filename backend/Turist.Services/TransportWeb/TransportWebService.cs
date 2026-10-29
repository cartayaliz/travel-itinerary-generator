using Turist.Domain.Distances;
using Turist.Domain.Graph;
using Turist.Domain.Interfaces.Graph;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Services.TransportWeb;

public class TransportWebService : ITransportWebService
{
    private readonly IItinerarioRepository _itinerarioRepository;
    private readonly IParadaRepository _paradaRepository;
    private IEnumerable<Itinerario> _orderedItinerarios = [];
    private readonly Dictionary<int, Parada> _paradasById = [];
    private readonly WeightedGraph<Weight> _graph = new();
    private readonly double _closeRadius;

    public TransportWebService(
        IItinerarioRepository itinerarioRepository, 
        IParadaRepository paradaRepository, 
        double closeRadius)
    {
        _itinerarioRepository = itinerarioRepository;
        _paradaRepository = paradaRepository;
        _closeRadius = closeRadius;
    }

    private bool TryWeightFunc(int from, int to, out Weight weight)
    {
        weight = Weight.AdditiveIdentity;
        weight = new(GeodesicCalculator.HaversineKm(
            _paradasById[from].Latitud, 
            _paradasById[from].Longitud, 
            _paradasById[to].Latitud, 
            _paradasById[to].Longitud));
        return weight.Cost <= _closeRadius;
    }

    public async Task LoadAsync()
    {
        _orderedItinerarios = await _itinerarioRepository.GetAllWithParadasOrderedAsync();
        var paradas = await _paradaRepository.GetAllAsync();
        foreach (var item in paradas)
        {
            _paradasById[item.ParadaID] = item;
        }
    }

    public void LoadGraph()
    {
        foreach (var item in _paradasById)
        {
            _graph.AddNode(item.Key);
        }
        foreach (var itinerario in _orderedItinerarios)
        {
            for (int i = 0; i < itinerario.ItinerarioParadas.Count - 1; i++)
            {
                int from = itinerario.ItinerarioParadas[i].ParadaID;
                int to = itinerario.ItinerarioParadas[i + 1].ParadaID;
                if (!TryWeightFunc(from, to, out Weight weight)) continue;
                _graph.AddEdge(from, to, weight);
            }
        }
    }

    public List<int> ProposeAlternatives()
    {
        throw new NotImplementedException();
    }
}
