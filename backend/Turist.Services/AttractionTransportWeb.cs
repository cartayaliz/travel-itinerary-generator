using System.Numerics;
using Turist.Domain.Classes;
using Turist.Domain.Interfaces;

namespace Turist.Services;

public class Weight: IComparable<Weight>, IAdditiveIdentity<Weight, Weight>, IAdditionOperators<Weight, Weight, Weight>
{
    public TimeSpan Time;
    public decimal Cost;

    public Weight(TimeSpan time, decimal cost)
    {
        Time = time;
        Cost = cost;
    }

    public static Weight AdditiveIdentity => new(TimeSpan.Zero, 0);

    public int CompareTo(Weight? other)
    {
        if (other is null) return 1;
        var timeComparison = Time.CompareTo(other.Time);
        if (timeComparison != 0) return timeComparison;
        return Cost.CompareTo(other.Cost);
    }

    public static Weight operator +(Weight left, Weight right)
    {
        return new(left.Time + right.Time, left.Cost + right.Cost);
    }
}

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

    public void AddActivityGraph(IActivity activity)
    {
        var n = _fGraph.NodesCount;
        _fGraph.AddNode(n);
        _bGraph.AddNode(n);
        _activityNodePairs[activity.Id] = n;
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

    public bool RemoveAttractionGraph(int id)
    {
        int node = _attractionNodePairs[id];
        return _fGraph.RemoveNode(node) && _bGraph.RemoveNode(node);
    }

    public void AddEdge(int nodeL, int nodeR, TimeSpan time, decimal cost)
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
            if (!_fGraph.TryGetWeight(act, nextAct, out Weight? w) || w is null)
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
    public int? ProposeAlternative(int origin, int destination, DateTime maxTime, decimal maxCost, Func<IActivity, bool> filterAct)
    {
        Weight[] fD = _fGraph.DijkstraGetMst(origin).distances;
        Weight[] bD = _bGraph.DijkstraGetMst(origin).distances;
        (Weight, int)? best = null;
        for (int i = 0; i < fD.Length; i++)
        {
            var temp = fD[i] + bD[i];
            if (best is null || best.Value.Item1.CompareTo(temp) > 0)
            {
                best = new(temp, i);
            }
        }
        if (best == null)
        {
            return null;
        }
        return best.Value.Item2;
    }
}
