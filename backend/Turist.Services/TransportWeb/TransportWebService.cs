using Turist.Domain.Distances;
using Turist.Domain.Graph;
using Turist.Domain.Interfaces.Graph;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Interfaces.Services;
using Turist.Domain.Models;

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
                _graph.AddEdge(to, from, weight);
            }
        }
    }

    public int[] ProposeAlternatives(
        int num,
        int originId,
        int destinationId,
        Weight maxWeight,
        Func<Parada, bool> filterAct)
    {
        //Chequeos
        if (!IsValidInput(
            num,
            originId,
            destinationId,
            maxWeight,
            filterAct))
            return [];

        //Dijkstra bidireccional
        var fDijkstra = _graph.DijkstraGetMst(originId);
        var bDijkstra = _graph.DijkstraGetMst(destinationId);

        //Crear cola de prioridad ascendente
        var queueComparer = Comparer<Weight>.Create((left, right) => left.Cost.CompareTo(right));
        var alternativesQueue = new PriorityQueue<int, Weight>(queueComparer);

        foreach (var id in _graph.Nodes)
        {
            if (id == originId
                || id == destinationId
                || !_graph.HasNode(id)
                || !filterAct(_paradasById[id])
                || !fDijkstra.hasDistance[id]
                || !bDijkstra.hasDistance[id])
                continue;

            Weight totalWeight = fDijkstra.distances[id] + bDijkstra.distances[id];
            if (totalWeight.CompareTo(maxWeight) > 0)
                continue;

            alternativesQueue.Enqueue(id, totalWeight);
            if (alternativesQueue.Count > num)
                alternativesQueue.Dequeue();
        }

        int[] alterArr = new int[alternativesQueue.Count];
        for (int i = alternativesQueue.Count - 1; i >= 0 ; i--)
        {
            alterArr[i] = alternativesQueue.Dequeue();
        }
        return alterArr;

        bool IsValidInput(
            int num,
            int originId,
            int destinationId,
            Weight maxWeight,
            Func<Parada, bool> filterAct)
        {
            return num > 0
                && filterAct is not null
                && maxWeight.CompareTo(Weight.AdditiveIdentity) > 0
                && _paradasById.ContainsKey(originId)
                && _paradasById.ContainsKey(destinationId);
        }
    }
}
