namespace ItineraryGenerator.ActivitiesGraph;

public class WeightedDirectedGraph
{
    private Dictionary<int, List<int>> _adj = [];
    private Dictionary<(int, int), double> _weight = [];

    public IReadOnlyCollection<int> Nodes => _adj.Keys;

    public List<int> GetNeighbors(int node)
    {
        if (_adj.TryGetValue(node, out var neighbors))
        {
            return neighbors;
        }
        else
        {
            throw new ArgumentException($"No existe {node}.");
        }
    }

    public void AddNode(int node) => _adj.TryAdd(node, []);

    public void RemoveNode(int node)
    {
        List<int> neighbors = GetNeighbors(node);
        foreach (var item in neighbors)
        {
            RemoveArc(node, item);
        }
        _adj.Remove(node);
    }

    public void AddArc(int node1, int node2, double weight)
    {
        AddNode(node1); 
        AddNode(node2);
        _adj[node1].Add(node2);
        _weight[(node1, node2)] = weight;
    }

    public void RemoveArc(int node1, int node2)
    {
        if (_adj.TryGetValue(node1, out List<int>? neighbors))
        {
            neighbors.Remove(node2);
        }
        _weight.Remove((node1, node2));
    }

    public bool HasArc(int node1, int node2) 
        => _weight.ContainsKey((node1, node2));

    public double GetWeight(int node1, int node2)
    {
        if (!_weight.TryGetValue((node1,node2), out double weight))
        {
            throw new ArgumentException($"No existe <{node1},{node2}>.");
        }
        else
        {
            return weight;
        }
    }

    public bool TryGetWeight(int node1, int node2, out double weight) 
        => _weight.TryGetValue((node1, node2), out weight);
}
