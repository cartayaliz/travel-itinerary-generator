using System.Data.SqlTypes;
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

    public int NodesCount { get => _adj.Keys.Count; }

    public int EdgesCount
    {
        get
        {
            int count = 0;
            foreach (var item in _adj)
            {
                if (item.Value.Count <= 0) continue;
                count += item.Value.Count;
            }
            return count;
        }
    }

    public IEnumerable<int> Nodes { get => _adj.Keys; }

    public IEnumerable<(int from, int to, TWeight weight)> Edges { get => GetEdges(); }

    public void AddNode(int node)
    {
        if (!_adj.ContainsKey(node))
        {
            _adj.Add(node, []);
        }
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
        yield break;
    }

    public void AddEdge(int from, int to, TWeight weight) => _adj[from].Add((to, weight));

    public int RemoveEdge(int from, int to)
    {
        if (_adj.TryGetValue(from, out var neighbors))
        {
            return neighbors.RemoveAll(n => n.to == to);
        }
        return 0;
    }

    public bool HasEdge(int from, int to) => _adj.ContainsKey(from) && _adj[from].Any(n => n.to == to);

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

    public (int[] pi, TWeight[] d) DijkstraGetMst(int from)
    {
        var V = Nodes;
        TWeight[] d = new TWeight[NodesCount];
        int[] pi = new int[NodesCount];

        InitSingleSource(from, d, pi, V);
        var minHeap = BuildMinHeap(V, d);
        List<int> S = [];
        while (minHeap.Count > 0)
        {
            var u = minHeap.Dequeue();
            S.Add(u);
            foreach (var (to, weight) in GetNeighbors(u))
            {
                Relax(u, to, weight);
            }
        }
        return (pi, d);

        void Relax(int u, int v, TWeight w)
        {
            if (d[v].CompareTo(d[u] + w) > 0)
            {
                d[v] = d[u] + w;
                pi[v] = u;
            }
        }

        void InitSingleSource(int from, TWeight?[] d, int[] pi, IEnumerable<int> V)
        {
            foreach (var n in V)
            {
                d[n] = default;
                pi[n] = int.MaxValue;
            }
            d[from] = TWeight.AdditiveIdentity;
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