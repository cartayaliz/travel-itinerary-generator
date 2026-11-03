using Turist.Domain.Distances;
using Turist.Domain.Graph;
using Turist.Domain.Models;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Interfaces.Services;

namespace Turist.Services.TransportWeb;

public class TransportWebService : ITransportWebService
{
    private readonly IItinerarioRepository _itinerarioRepository;
    private readonly IParadaRepository _paradaRepository;
    private IEnumerable<Itinerario> _orderedItinerarios = [];
    private readonly Dictionary<int, Parada> _paradasById = [];
    private readonly WeightedGraph<Weight> _graph = new();
    private readonly double _closeRadiusKm;

    public TransportWebService(
        IItinerarioRepository itinerarioRepository,
        IParadaRepository paradaRepository,
        double closeRadiusKm)
    {
        _itinerarioRepository = itinerarioRepository;
        _paradaRepository = paradaRepository;
        _closeRadiusKm = closeRadiusKm;
    }

    private bool TryWeightFunc(int from, int to, out Weight weight)
    {
        weight = Weight.AdditiveIdentity;
        weight = new(GeodesicCalculator.HaversineKm(
            _paradasById[from].Latitud,
            _paradasById[from].Longitud,
            _paradasById[to].Latitud,
            _paradasById[to].Longitud));
        return weight.Cost <= _closeRadiusKm;
    }

    public async Task LoadAsync()
    {
        _orderedItinerarios = await _itinerarioRepository.GetAllWithParadasOrderedAsync();
        var paradas = await _paradaRepository.GetAllAsync();
        _paradasById.Clear();
        foreach (var item in paradas)
        {
            _paradasById[item.ParadaID] = item;
        }
        BuildGraph();
    }

    private void BuildGraph()
    {
        _graph.Clear();
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
                _graph.AddEdge(to, from, weight);
            }
        }
    }

    public List<int> ProposeAlternatives(
        int maxStopsCnt,
        int originId,
        int destinationId,
        Weight maxWeight,
        Func<Parada, bool> filterStop)
    {
        if (!IsValidInput(
            maxStopsCnt,
            originId,
            destinationId,
            maxWeight,
            filterStop))
            return [];

        var fDijkstra = _graph.Dijkstra(originId);
        var bDijkstra = _graph.Dijkstra(destinationId);

        var queueComparer = Comparer<Weight>
            .Create((left, right) => left.Cost.CompareTo(right));
        var altersQueue = new PriorityQueue<int, Weight>(queueComparer);

        foreach (var id in _graph.Nodes)
        {
            if (id == originId
                || id == destinationId
                || !_graph.ContainsNode(id)
                || !_paradasById.TryGetValue(id, out var paradaAlt)
                || !filterStop(paradaAlt)
                || !fDijkstra.hasDistance.TryGetValue(id, out var fd)
                || !fd
                || bDijkstra.hasDistance.TryGetValue(id, out var bd)
                || !bd
                || !fDijkstra.distances.TryGetValue(id, out var fWeight)
                || !bDijkstra.distances.TryGetValue(id, out var bWeight))
                continue;

            var totalWeight = fWeight + bWeight;
            if (totalWeight.CompareTo(maxWeight) > 0)
                continue;

            altersQueue.Enqueue(id, totalWeight);
            if (altersQueue.Count > maxStopsCnt)
                altersQueue.Dequeue();
        }

        List<int> altersArr = new List<int>();
        for (int i = altersQueue.Count - 1; i >= 0; i--)
        {
            altersArr.Add(altersQueue.Dequeue());
        }
        return altersArr;

        bool IsValidInput(
            int maxStopsCnt,
            int originId,
            int destinationId,
            Weight maxWeight,
            Func<Parada, bool> filterStop)
        {
            return maxStopsCnt > 0
                && filterStop is not null
                && maxWeight.CompareTo(Weight.AdditiveIdentity) > 0
                && _paradasById.ContainsKey(originId)
                && _paradasById.ContainsKey(destinationId);
        }
    }
}
