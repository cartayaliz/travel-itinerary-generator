using Turist.Domain.Interfaces;

namespace Turist.Domain.Classes;

public class UnweightedGraph : IUnweightedGraph
{
    private readonly Dictionary<int, List<int>> _adj = [];
    public int NodesCount => _adj.Keys.Count;
    public IEnumerable<int> Nodes => _adj.Keys;

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

        foreach (var v in neighbors)
        {
            if (v == node) continue;
            _adj[v].RemoveAll(n => n == node);
        }

        _adj.Remove(node);
        return true;
    }

    public bool HasNode(int node) => _adj.ContainsKey(node);

    public IEnumerable<(int from, int To)> Edges => GetEdges();

    private IEnumerable<(int from, int To)> GetEdges()
    {
        foreach (var kvp in _adj)
        {
            foreach (var to in kvp.Value)
            {
                yield return (kvp.Key, to);
            }
        }
    }

    public IEnumerable<int> GetNeighbors(int from)
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

    public void AddEdge(int from, int to) => _adj[from].Add(to);

    public bool RemoveEdge(int from, int to)
    {
        if (_adj.TryGetValue(from, out var neighbors))
        {
            neighbors.RemoveAll(n => n == to);
            return true;
        }
        return false;
    }

    public bool HasEdge(int from, int to) 
        => _adj.ContainsKey(from) && _adj[from].Any(n => n == to);
}