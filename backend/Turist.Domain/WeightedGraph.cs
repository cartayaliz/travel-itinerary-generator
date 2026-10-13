using Turist.Domain.Interfaces;
using static System.Double;

namespace Turist.Domain;

public class WeightedGraph : IWeightedGraph
{
    private readonly Dictionary<int, List<(int to, double weight)>> _adj = [];

    public int GetNodesCount() => _adj.Keys.Count;

    public int GetEdgesCount() => _adj.Values.Count;

    public IEnumerable<int> GetNodes() => _adj.Keys;

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

    public IEnumerable<(int from, int to, double weight)> GetEdges()
        => from kvp in _adj from neighbor in kvp.Value select (kvp.Key, neighbor.to, neighbor.weight);

    public void AddEdge(int from, int to, double weight) => _adj[from].Add((to, weight));

    public int RemoveEdge(int from, int to)
    {
        if (_adj.TryGetValue(from, out var neighbors))
        {
            return neighbors.RemoveAll(n => n.to == to);
        }
        return 0;
    }

    public bool HasEdge(int from, int to) => _adj.ContainsKey(from) && _adj[from].Any(n => n.to == to);

    public IEnumerable<(int to, double weight)> GetNeighbors(int from)
        => _adj.TryGetValue(from, out var neighbors) ? neighbors : throw new KeyNotFoundException();

    public double GetWeight(int from, int to)
    {
        if (!_adj.TryGetValue(from, out var neighbors)) throw new ArgumentNullException();
        var edge = neighbors.FirstOrDefault(e => e.to == to);
        return edge != default((int, double)) ? edge.weight : throw new ArgumentNullException();
    }

    public bool TryGetWeight(int from, int to, out double weight)
    {
        weight = NaN;
        if (!_adj.TryGetValue(from, out var neighbors)) return false;
        foreach (var v in neighbors)
        {
            if (v.to != to) continue;
            weight = v.weight;
            return true;
        }

        return false;
    }

    public bool SetWeight(int from, int to, double newWeight)
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
}