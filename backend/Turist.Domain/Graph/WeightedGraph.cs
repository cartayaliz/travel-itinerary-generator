using System.Numerics;

namespace Turist.Domain.Graph;

public readonly record struct Weight(double Cost) :
    IAdditiveIdentity<Weight, Weight>,
    IAdditionOperators<Weight, Weight, Weight>,
    IComparable<Weight>
{
    public static Weight AdditiveIdentity => new(0);

    public int CompareTo(Weight other) => Cost.CompareTo(other.Cost);

    public static Weight operator +(Weight left, Weight right) => new(left.Cost + right.Cost);
}

/// <summary>
/// Representa un grafo ponderado.
/// </summary>
public class WeightedGraph<TWeight> where TWeight :
    IAdditiveIdentity<TWeight, TWeight>,
    IAdditionOperators<TWeight, TWeight, TWeight>,
    IComparable<TWeight>
{
    private readonly Dictionary<int, List<(int to, TWeight weight)>> _adj = [];

    public int NodesCount => _adj.Keys.Count;

    public int EdgesCount
    {
        get
        {
            var count = 0;
            foreach (var item in _adj)
            {
                if (item.Value.Count <= 0) continue;
                count += item.Value.Count;
            }
            return count;
        }
    }

    public IEnumerable<int> Nodes => _adj.Keys;

    public IEnumerable<(int from, int to, TWeight weight)> Edges
    {
        get
        {
            foreach (var kvp in _adj)
            {
                foreach (var (to, weight) in kvp.Value)
                {
                    yield return (kvp.Key, to, weight);
                }
            }
        }
    }

    public void AddNode(int id) => _adj[id] = [];

    public bool RemoveNode(int node)
    {
        if (!_adj.ContainsKey(node))
            return false;

        foreach (var neighbors in _adj.Values)
            neighbors.RemoveAll(edge => edge.to == node);

        _adj.Remove(node);
        return true;
    }

    public bool HasNode(int node) => _adj.ContainsKey(node);

    public void AddEdge(int from, int to, TWeight weight)
    {
        if (!_adj.ContainsKey(from))
            _adj[from] = [];

        _adj[from].Add((to, weight));
    }

    public bool RemoveEdge(int from, int to)
    {
        if (_adj.TryGetValue(from, out var neighbors))
        {
            neighbors.RemoveAll(n => n.to == to);
            return true;
        }
        return false;
    }

    public bool HasEdge(int from, int to)
        => _adj.ContainsKey(from) && _adj[from].Any(n => n.to == to);

    public IEnumerable<(int to, TWeight weight)> GetNeighbors(int from)
    {
        if (_adj.TryGetValue(from, out var neighbors))
            return neighbors;
        else
            throw new KeyNotFoundException();
    }

    public TWeight? GetWeight(int from, int to)
    {
        if (!_adj.TryGetValue(from, out var neighbors)) return default;
        (int to, TWeight weight) edge = neighbors.FirstOrDefault(e => e.to == to);
        return !edge.Equals(default) ? edge.weight : default;
    }

    public bool TryGetWeight(int from, int to, out TWeight? weight)
    {
        weight = default;
        if (!_adj.TryGetValue(from, out var neighbors)) return false;
        foreach (var v in neighbors)
        {
            if (v.to != to) continue;
            weight = v.weight;
            return true;
        }

        return false;
    }

    public bool SetWeight(int from, int to, TWeight newWeight)
    {
        if (!_adj.TryGetValue(from, out var neighbors)) return false;
        for (var i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i].to != to) continue;
            neighbors[i] = (to, newWeight);
            return true;
        }
        return false;
    }

    public (Dictionary<int, int> parents, Dictionary<int, TWeight> distances, Dictionary<int, bool> hasDistance) DijkstraGetMst(int source)
    {
        InitSingleSource(out var hasDistance, out var distances, out var parents);
        if (!hasDistance.TryGetValue(source, out var sourceHas) || !sourceHas) 
            return (parents, distances, hasDistance);

        var queue = new PriorityQueue<int, TWeight>(Comparer<TWeight>.Create((left, right) => left.CompareTo(right)));
        queue.Enqueue(source, distances[source]);
        while (queue.TryDequeue(out var u, out var queuedDistance))
        {
            if (!hasDistance.TryGetValue(u, out var uHas) || !uHas || queuedDistance.CompareTo(distances[u]) != 0)
                continue;

            foreach (var (to, weight) in GetNeighbors(u))
            {
                Relax(u, to, weight);
            }
        }
        return (parents, distances, hasDistance);

        void Relax(int u, int to, TWeight weight)
        {
            var candidate = distances[u] + weight;

            if (!hasDistance.TryGetValue(to, out var toHas) || !toHas || candidate.CompareTo(distances[to]) < 0)
            {
                distances[to] = candidate;
                hasDistance[to] = true;
                parents[to] = u;
                queue.Enqueue(to, candidate);
            }
        }

        void InitSingleSource(out Dictionary<int, bool> hasDistance, out Dictionary<int, TWeight> distances, out Dictionary<int, int> parents)
        {
            parents = [];
            distances = [];
            hasDistance = [];
            distances[source] = TWeight.AdditiveIdentity;
            parents[source] = -1;
            hasDistance[source] = _adj.ContainsKey(source);
        }
    }
}