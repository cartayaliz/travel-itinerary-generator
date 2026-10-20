using System.Numerics;
using Turist.Domain.Classes;
using Turist.Domain.Interfaces;

namespace Turist.Services;

public class AttractionTransportWeb
{
    private readonly WeightedGraph<Weight> _fGraph;
    private readonly WeightedGraph<Weight> _bGraph;
    private readonly Dictionary<int, int> _activityNodePairs = [];
    private readonly Dictionary<int, int> _attractionNodePairs = [];
    private readonly Func<int, IActivity> _activityById;
    private readonly Func<int, IAttraction> _attractionById;

    public AttractionTransportWeb(
        Func<int, IActivity> activityById,
        Func<int, IAttraction> attractionById)
    {
        _fGraph = new();
        _bGraph = new();
        _activityById = activityById;
        _attractionById = attractionById;
    }

    public int ActivityNode(int id) => _activityNodePairs[id];

    public bool TryActivityNode(int id, out int node)
    {
        if (!_activityNodePairs.TryGetValue(id, out int value))
        {
            node = 0;
            return false;
        }
        node = value;
        return true;
    }

    public int AddActivityGraph(int id)
    {
        if (_activityNodePairs.TryGetValue(id, out var existingNode))
            return existingNode;
        
        _fGraph.AddNode();
        return _activityNodePairs[id] = _bGraph.AddNode();
    }

    public bool RemoveActivityGraph(int id)
    {
        if (!_activityNodePairs.TryGetValue(id, out var node))
            return false;

        var removedFromForward = _fGraph.RemoveNode(node);
        var removedFromBackward = _bGraph.RemoveNode(node);
        if (!removedFromForward || !removedFromBackward)
            return false;

        return _activityNodePairs.Remove(id);
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

    public int AddAttractionGraph(int id)
    {
        if (_attractionNodePairs.TryGetValue(id, out var existingNode))
            return existingNode;

        _fGraph.AddNode();
        return _attractionNodePairs[id] = _bGraph.AddNode();
    }

    public bool RemoveAttractionGraph(int id)
    {
        if (!_attractionNodePairs.TryGetValue(id, out var node))
            return false;

        if (!_fGraph.RemoveNode(node) || !_bGraph.RemoveNode(node))
            return false;

        return _attractionNodePairs.Remove(id);
    }

    public void AddConexion(int nodeL, int nodeR, TimeSpan time, double cost)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(time, TimeSpan.Zero);
        ArgumentOutOfRangeException.ThrowIfNegative(cost);
        var weight = new Weight(time, cost);
        _fGraph.AddEdge(nodeL, nodeR, weight);
        _bGraph.AddEdge(nodeR, nodeL, weight);
    }

    public bool RemoveEdge(int nodeL, int nodeR) 
        => _fGraph.RemoveEdge(nodeL, nodeR) && _bGraph.RemoveEdge(nodeR, nodeL);

    public double CalculateCost(IItinerary itinerary)
    {
        double totalCost = 0;
        var activities = itinerary.Activities;
        for (int i = 0; i < activities.Count - 1; i++)
        {
            var act = _activityNodePairs[activities[i]];
            var nextAct = _activityNodePairs[activities[i + 1]];
            if (!_fGraph.TryGetWeight(act, nextAct, out Weight w))
                return double.MaxValue;
            totalCost += w.Cost;
        }
        return totalCost;
    }

    public List<(Weight weight, int activityId)> ProposeAlternatives(
        int num,
        int originActId,
        int destinationActId,
        TimeSpan maxTime,
        double maxCost,
        Func<int, bool> filterAct,
        double timeShare = 0.5)
    {
        //Chequeos
        if (!IsValidInput(
            num,
            originActId,
            destinationActId,
            maxTime,
            maxCost,
            filterAct,
            timeShare,
            out int originNode,
            out int destinationNode))
            return [];

        //Dijkstra bidireccional
        var fDijkstra = _fGraph.DijkstraGetMst(originNode, Compare);
        var bDijkstra = _bGraph.DijkstraGetMst(destinationNode, Compare);

        //Crear cola de prioridad ascendente
        var queueComparer = Comparer<AlternativePriority>.Create((left, right) => ComparePriority(right, left));
        var alternativesQueue = new PriorityQueue<(Weight weight, int activityId), AlternativePriority>(queueComparer);

        foreach (var (activityId, node) in _activityNodePairs)
        {
            if (node == originNode || node == destinationNode || !_fGraph.HasNode(node) || !filterAct(activityId))
                continue;
            if (!fDijkstra.hasDistance[node] || !bDijkstra.hasDistance[node])
                continue;

            var totalWeight = fDijkstra.distances[node] + bDijkstra.distances[node];
            if (totalWeight.Time > maxTime || totalWeight.Cost > maxCost)
                continue;

            var priority = new AlternativePriority(
                Weight.Score(totalWeight, maxTime, maxCost, timeShare),
                totalWeight.Time,
                totalWeight.Cost,
                activityId);
            alternativesQueue.Enqueue((totalWeight, activityId), priority);
            if (alternativesQueue.Count > num)
                alternativesQueue.Dequeue();
        }

        var alternatives = new List<(Weight weight, int activityId)>(alternativesQueue.Count);
        while (alternativesQueue.TryDequeue(out var alternative, out _))
            alternatives.Add(alternative);
        alternatives.Reverse();
        return alternatives;

        int Compare(Weight w1, Weight w2)
            => Weight.Compare(w1, w2, maxTime, maxCost, timeShare);

        int ComparePriority(AlternativePriority left, AlternativePriority right)
        {
            var scoreComparison = left.Score.CompareTo(right.Score);
            if (scoreComparison != 0)
                return scoreComparison;

            var costComparison = left.Cost.CompareTo(right.Cost);
            if (costComparison != 0)
                return costComparison;

            var timeComparison = left.Time.CompareTo(right.Time);
            return timeComparison != 0
                ? timeComparison
                : left.ActivityId.CompareTo(right.ActivityId);
        }

        bool IsValidInput(
            int num,
            int originActId,
            int destinationActId,
            TimeSpan maxTime,
            double maxCost,
            Func<int, bool> filterAct,
            double timeShare,
            out int originNode,
            out int destinationNode)
        {
            originNode = 0;
            destinationNode = 0;
            ArgumentOutOfRangeException.ThrowIfNegative(num);
            ArgumentNullException.ThrowIfNull(filterAct);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxTime, TimeSpan.Zero);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxCost);
            if (timeShare is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(timeShare));
            if (num == 0)
                return false;
            if (!_activityNodePairs.TryGetValue(originActId, out originNode))
                throw new ArgumentOutOfRangeException(nameof(originActId), "The origin activity does not exist.");
            if (!_activityNodePairs.TryGetValue(destinationActId, out destinationNode))
                throw new ArgumentOutOfRangeException(nameof(destinationActId), "The destination activity does not exist.");
            return true;
        }
    }

    private readonly record struct AlternativePriority(
        double Score,
        TimeSpan Time,
        double Cost,
        int ActivityId);
}
