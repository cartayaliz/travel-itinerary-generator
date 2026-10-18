using System.Numerics;
using Turist.Domain.Interfaces;

namespace Turist.Domain.Classes;

/// <summary>
/// Representa un grafo ponderado dirigido.
/// </summary>
public class WeightedGraph<TWeight> : IWeightedGraph<TWeight> where TWeight : IComparable<TWeight>,
                                                                              IAdditiveIdentity<TWeight, TWeight>,
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
        var n = _adj.Keys.Count;
        _adj.Add(n, []);
        return n;
    }

    public bool RemoveNode(int node)
    {
        if (!_adj.TryGetValue(node, out var neighbors))
            return false;

        foreach (var (neighbor, _) in neighbors)
        {
            if (neighbor == node) continue;
            _adj[neighbor].RemoveAll(n => n.to == node);
        }
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
        {
            return neighbors;
        }
        else
        {
            throw new KeyNotFoundException();
        }
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

    public (int[] parents, TWeight[] distances, bool[] hasDistance) DijkstraGetMst(int source)
    {
        var distances = new TWeight[NodesCount];
        var hasDistance = new bool[NodesCount];
        int[] parents = new int[NodesCount];
        
        for (int i = 0; i < NodesCount; i++)
        {
            parents[i] = -1;
        }
        hasDistance[source] = true;
        distances[source] = TWeight.AdditiveIdentity;

        var queue = new PriorityQueue<int, TWeight>();
        queue.Enqueue(source, distances[source]);
        while (queue.TryDequeue(out var u, out var queuedDistance))
        {
            if (!hasDistance[u] || queuedDistance.CompareTo(distances[u]) != 0)
                continue;

            foreach (var (to, weight) in GetNeighbors(u))
            {
                var candidate = distances[u] + weight;

                if (!hasDistance[to] ||
                    candidate.CompareTo(distances[to]) < 0)
                {
                    distances[to] = candidate;
                    hasDistance[to] = true;
                    parents[to] = u;
                    queue.Enqueue(to, candidate);
                }
            }
        }
        return (parents, distances, hasDistance);

        void Relax(int u, int v, TWeight weight)
        {
            if (!hasDistance[v] || (distances[u] + weight).CompareTo(distances[v]) < 0)
            {
                distances[v] = distances[u] + weight;
                hasDistance[v] = true;
            }
        }

        void InitSingleSource(int source, TWeight?[] d, int[] pi, IEnumerable<int> V)
        {
            foreach (var n in V)
            {
                d[n] = default;
                pi[n] = -1;
            }
            distances[source] = TWeight.AdditiveIdentity;
            hasDistance[source] = true;
        }

        PriorityQueue<int, TWeight> BuildMinHeap(IEnumerable<int> v, TWeight[] d)
        {
            List<(int, TWeight)> items = [];
            foreach (var item in v)
            {
                items.Add((item, d[item]));
            }
            return new PriorityQueue<int, TWeight>(items);
        }
    }
}