namespace Turist.Domain.Interfaces;
public interface INodeSet
{
    int GetNodesCount();
    int GetEdgesCount();
    IEnumerable<int> GetNodes();
    void AddNode(int node);
    bool RemoveNode(int node);
    bool HasNode(int node);
}

public interface IWeightedGraph : INodeSet
{
    IEnumerable<(int from, int to, double weight)> GetEdges();
    IEnumerable<(int to, double weight)> GetNeighbors(int from);
    void AddEdge(int from, int to, double weight);
    int RemoveEdge(int from, int to);
    bool HasEdge(int from, int to);
    double GetWeight(int from, int to);
    bool SetWeight(int from, int to, double newWeight);
    bool TryGetWeight(int from, int to, out double weight);
}

// public interface IWeightlessGraph : INodeSet
// {
//     IEnumerable<(int from, int To)> GetEdges();
//     IEnumerable<int> GetNeighbors(int from);
//     void AddEdge(int from, int to);
//     void RemoveEdge(int from, int to);
//     bool HasEdge(int from, int to);
// }