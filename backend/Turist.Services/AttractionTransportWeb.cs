using System.Numerics;
using Turist.Domain.Classes;
using Turist.Domain.Interfaces;

namespace Turist.Services;

public class AttractionTransportWeb
{
    private readonly WeightedGraph<Weight> _fGraph;
    private readonly WeightedGraph<Weight> _bGraph;
    private readonly Dictionary<int, int> _activityNodePairs = [];
    private readonly Dictionary<int, int> _attractionNodePairs = [];
    // private readonly Dictionary<(int, int), (TimeSpan time, decimal cost)> _timeCostEdgePairs = [];
    private readonly Func<int, IActivity> _activityById;
    private readonly Func<int, IAttraction> _attractionById;

    public AttractionTransportWeb(Func<int, IActivity> activityById, Func<int, IAttraction> attractionById)
    {
        _activityById = activityById;
        _attractionById = attractionById;
        _fGraph = new();
        _bGraph = new();
    }

    public int ActivityNode(int id) => _activityNodePairs[id];

    public bool TryActivityNode(int id, out int node)
    {
        if (!_activityNodePairs.ContainsKey(id))
        {
            node = 0;
            return false;
        }
        node = _activityNodePairs[id];
        return true;
    }

    public int AddActivityGraph(int id)
    {
        _fGraph.AddNode();
        return _activityNodePairs[id] = _bGraph.AddNode();
    }

    public bool RemoveActivityGraph(int id)
    {
        int node = _activityNodePairs[id];
        return _fGraph.RemoveNode(node) && _bGraph.RemoveNode(node);
    }

    public int AttractionNode(int id) => _attractionNodePairs[id];

    public bool TryAttractionNode(int id, out int node)
    {
        if (!_attractionNodePairs.TryGetValue(id, out int n))
        {
            node = 0;
            return false;
        }
        node = n;
        return true;
    }

    public int AddAttractionGraph(int id)
    {
        _fGraph.AddNode();
        return _attractionNodePairs[id] = _bGraph.AddNode();
    }

    public bool RemoveAttractionGraph(int id)
    {
        int node = _attractionNodePairs[id];
        return _fGraph.RemoveNode(node) && _bGraph.RemoveNode(node);
    }

    public void AddTransport(int nodeL, int nodeR, TimeSpan time, decimal cost)
    {
        var weight = new Weight(time, cost);
        _fGraph.AddEdge(nodeL, nodeR, weight);
        _bGraph.AddEdge(nodeR, nodeL, weight);
    }

    public bool RemoveEdge(int nodeL, int nodeR)
    {
        return _fGraph.RemoveEdge(nodeL, nodeR) && _bGraph.RemoveEdge(nodeR, nodeL);
    }

    public decimal CalculateCost(IItinerary itinerary)
    {
        decimal totalCost = 0;
        var activities = itinerary.Activities;
        for (int i = 0; i < activities.Count - 1; i++)
        {
            var act = _activityNodePairs[activities[i]];
            var nextAct = _activityNodePairs[activities[i + 1]];
            if (!_fGraph.TryGetWeight(act, nextAct, out Weight w))
            {
                return decimal.MaxValue;
            }
            totalCost += w.Cost;
        }
        return totalCost;
    }

    /*
    proponer-alternativa(origen, destino, max-tiempo, max-costo, filtro):
        d-anverso <- dijkstra(grafo, origen)
        d-reverso <- dijkstra(grafo-invertido, destino)
        f <- d-anverso + d-reverso
        (m-f, mejor-nodo) <- nil
        por cada n en V(G):
            si (m-f, mejor) OR f[n] < m-f: 
                m-f <- f[n]
        retorno mejor-nodo
    */
    /// <summary>
    /// Busca actividad tal que:
    ///     - pertenezca a un camino minimal desde origen hasta destino.
    ///     - no cumpla el filtro.
    ///     - distinta al origen y destino.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    /// <param name="filterAct"></param>
    public int? ProposeAlternative(int origin, int destination, Func<int, bool> filterAct)
    /// <returns></returns>
    {
        var fDijkstra = _fGraph.DijkstraGetMst(origin);
        var bDijkstra = _bGraph.DijkstraGetMst(destination);
        
        if (!fDijkstra.hasDistance[destination] || fDijkstra.parents[destination] == origin)
            return null;



        (Weight weight, int node)? best = null;
        for (int i = 0; i < _fGraph.NodesCount; i++)
        {
            if (i == origin || i == destination || !filterAct(i)) continue;

            var temp = fDijkstra.distances[i] + bDijkstra.distances[i];
            if (best is null || best.Value.weight.CompareTo(temp) > 0)
                best = new(temp, i);
        }
        return best?.node;
    }
}
