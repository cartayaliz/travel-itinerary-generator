using Turist.Domain.Classes;
using Turist.Domain.Interfaces;

namespace Turist.Services;

public class AttractionTransportWeb
{
    private readonly UnweightedGraph _graph = new();
    private readonly Dictionary<int, (int n, IActivity act)> _activityNodePairs = [];
    private readonly Dictionary<int, (int n, IAttraction att)> _attractionNodePairs = [];
    private readonly Dictionary<(int, int), (TimeSpan time, decimal cost)> _timeCostEdgePairs = [];

    public int ActivityNode(int id) => _activityNodePairs[id].n;

    public bool TryActivityNode(int id, out int node)
    {
        if (!_activityNodePairs.ContainsKey(id))
        {
            node = 0;
            return false;
        }
        node = _activityNodePairs[id].n;
        return true;
    }

    public void AddActivityGraph(IActivity activity)
    {
        int id = activity.Id;
        var n = _graph.NodesCount;
        _graph.AddNode(n);
        _activityNodePairs[id] = (n, activity);
    }

    public bool RemoveActivityGraph(int id) => _graph.RemoveNode(_activityNodePairs[id].n);

    public int AttractionNode(int id) => _attractionNodePairs[id].n;

    public bool TryAttractionNode(int id, out int node)
    {
        if (!_attractionNodePairs.ContainsKey(id))
        {
            node = 0;
            return false;
        }
        node = _attractionNodePairs[id].n;
        return true;
    }

    public bool RemoveAttractionGraph(int id) => _graph.RemoveNode(_attractionNodePairs[id].n);

    public void AddEdge(int nodeL, int nodeR, TimeSpan time, decimal cost)
    {
        _graph.AddEdge(nodeL, nodeR);
        _timeCostEdgePairs[(nodeL, nodeR)] = (time, cost);
    }

    public bool RemoveEdge(int nodeL, int nodeR)
    {
        if (!_timeCostEdgePairs.Remove((nodeL, nodeR))) return false; 
        return _graph.RemoveEdge(nodeL, nodeR);
    }

    public decimal CalcCost(IItinerary itinerary)
    {
        decimal cost = 0;
        var activities = itinerary.Activities;
        for (int i = 0; i < activities.Count - 1; i++)
        {
            var act = _activityNodePairs[activities[i]].n;
            var nextAct = _activityNodePairs[activities[i + 1]].n;
            cost += _timeCostEdgePairs[(act, nextAct)].cost;
        }
        return cost;
    }

    public TimeSpan CalcTime(IItinerary itinerary)
    {
        TimeSpan time = TimeSpan.FromSeconds(0);
        var activities = itinerary.Activities;
        for (int i = 0; i < activities.Count - 1; i++)
        {
            var act = _activityNodePairs[activities[i]].n;
            var nextAct = _activityNodePairs[activities[i + 1]].n;
            time += _timeCostEdgePairs[(act, nextAct)].time;
        }
        return time;
    }

    public (double distance, IActivity activity)? RecommendActivity(IGeoPoint origin, double radius, Func<IActivity, bool> filterAct)
    {
        (double dist, IActivity act)? nearestAct = null;
        foreach (var n in _graph.Nodes)
        {
            var act = _activityNodePairs[n].act;
            if (filterAct(act)) continue;
            var nodeLat = act.GeoPoint.Latitude;
            var nodeLon = act.GeoPoint.Longitude;
            var dist = IGeoPoint.HaversineKm(nodeLat, origin.Latitude, nodeLon, origin.Longitude);
            if (dist > radius) continue;
            if (nearestAct == null || dist < nearestAct.Value.dist)
            {
                nearestAct = (dist, act);
            }
        }
        return nearestAct;
    }
}
