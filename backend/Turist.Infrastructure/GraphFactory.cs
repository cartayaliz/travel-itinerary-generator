using Turist.Domain;
using Turist.Domain.Interfaces;

namespace Turist.Infrastructure;

public static class GraphFactory
{
    private static double WeightFunction(IAvailable transportation)
    {
        throw new NotImplementedException();
    }
    
    private static int GetActivityId(int locationId)
    {
        return locationId;
    }
    
    private static int GetAttractionId(int locationId)
    {
        return locationId;
    }
    
    public static IWeightedGraph Build(IEnumerable<ILocation> activities, 
                                IEnumerable<ILocation> attractions, 
                                IEnumerable<IAvailable> availableTransportations)
    {
        var graph = new WeightedGraph();
        foreach (var activity in activities)
        {
            var node = activity.LocationId;
            graph.AddNode(GetActivityId(node));
        }
        foreach (var attraction in attractions)
        {
            var node = attraction.LocationId;
            graph.AddNode(GetAttractionId(node));
        }
        foreach (var avail in availableTransportations)
        {
            var weight = WeightFunction(avail);
            var n1 = avail.LocationIdLeft;
            var n2 = avail.LocationIdRight;
            graph.AddEdge(GetActivityId(n1), GetAttractionId(n2), weight);
        }
        return graph;
    }
}