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

    public void AddNode(int node) => _adj[node] = [];

    public void RemoveNode(int node)
    {
        foreach (var neighbors in _adj.Values)
            neighbors.RemoveAll(edge => edge.to == node);
        _adj.Remove(node);
    }

    public void Clear() => _adj.Clear();

    public bool ContainsNode(int node) => _adj.ContainsKey(node);

    public void AddEdge(int from, int to, TWeight weight) => _adj[from].Add((to, weight));

    public void RemoveEdge(int from, int to) => _adj[from].RemoveAll(n => n.to == to);

    public bool ContainsEdge(int from, int to) 
        => _adj.TryGetValue(from, out var neighbors) && neighbors.Any(n => n.to == to);

    public IEnumerable<(int to, TWeight weight)> GetNeighbors(int from) => _adj[from];

    public TWeight? GetWeight(int from, int to) => _adj[from].FirstOrDefault(e => e.to == to).weight;

    public void SetWeight(int from, int to, TWeight newWeight)
    {
        var neighbors = _adj[from];
        for (var i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i].to != to) continue;
            neighbors[i] = (to, newWeight);
        }
    }

    public (Dictionary<int, int> parents, Dictionary<int, TWeight> distances, Dictionary<int, bool> hasDistance) Dijkstra(int source)
    {
        InitSingleSource(out var hasDistance, out var distances, out var parents);
        if (!hasDistance.TryGetValue(source, out var sourceHas) || !sourceHas) 
            return (parents, distances, hasDistance);

        var queue = new PriorityQueue<int, TWeight>(
            Comparer<TWeight>.Create((left, right) => left.CompareTo(right)));
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