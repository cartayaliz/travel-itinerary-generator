using System.Numerics;
using Turist.Domain.Interfaces;

namespace Turist.Domain.Classes;

/// <summary>
/// Representa un grafo ponderado dirigido.
/// </summary>
public class WeightedGraph<TWeight> : IWeightedGraph<TWeight> where TWeight : IAdditiveIdentity<TWeight, TWeight>,
                                                                              IAdditionOperators<TWeight, TWeight, TWeight>
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

    public IEnumerable<(int from, int to, TWeight weight)> Edges => GetEdges();

    public int AddNode()
    {
        var n = _adj.Count == 0 ? 0 : _adj.Keys.Max() + 1;
        _adj.Add(n, []);
        return n;
    }

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

    private IEnumerable<(int from, int to, TWeight weight)> GetEdges()
    {
        foreach (var kvp in _adj)
        {
            foreach (var (to, weight) in kvp.Value)
            {
                yield return (kvp.Key, to, weight);
            }
        }
    }

    public void AddEdge(int from, int to, TWeight weight) => _adj[from].Add((to, weight));

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

    public (int[] parents, TWeight[] distances, bool[] hasDistance) DijkstraGetMst(int source, Func<TWeight, TWeight, int> comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);
        if (!_adj.ContainsKey(source))
            throw new ArgumentOutOfRangeException(nameof(source), "The source node does not exist in the graph.");

        InitSingleSource(out bool[] hasDistance, out TWeight[] distances, out int[] parents);

        var queueComparer = Comparer<TWeight>.Create((left, right) => comparer(left, right));
        var queue = new PriorityQueue<int, TWeight>(queueComparer);
        queue.Enqueue(source, distances[source]);
        while (queue.TryDequeue(out var u, out var queuedDistance))
        {
            if (!hasDistance[u] || comparer(queuedDistance, distances[u]) != 0)
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

            if (!hasDistance[to] || comparer(candidate, distances[to]) < 0)
            {
                distances[to] = candidate;
                hasDistance[to] = true;
                parents[to] = u;
                queue.Enqueue(to, candidate);
            }
        }

        void InitSingleSource(out bool[] hasDistance, out TWeight[] distances, out int[] parents)
        {
            var arrayLength = _adj.Keys.Max() + 1;
            distances = new TWeight[arrayLength];
            hasDistance = new bool[arrayLength];
            parents = new int[arrayLength];
            Array.Fill(parents, -1);
            hasDistance[source] = true;
            distances[source] = TWeight.AdditiveIdentity;
        }
    }
}