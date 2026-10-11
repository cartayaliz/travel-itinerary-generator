using Turist.Domain;
using Turist.Domain.Interfaces;

namespace Turist.Api;

public class GraphFactory
{
    private double WeightFunction(IAvailableTransportation transportation)
    {
        throw new NotImplementedException();
    }
    
    private int CreateActivityId(int locationId)
    {
        throw new NotImplementedException();
    }
    
    private int CreateAttractionId(int locationId)
    {
        throw new NotImplementedException();
    }
    
    public IWeightedGraph Build(IEnumerable<ILocation> activities, 
                                IEnumerable<ILocation> attractions, 
                                IEnumerable<IAvailableTransportation> availableTransportations)
    {
        var graph = new WeightedGraph();
        foreach (var activity in activities)
        {
            var node = activity.LocationId;
            graph.AddNode(CreateActivityId(node));
        }
        foreach (var attraction in attractions)
        {
            var node = attraction.LocationId;
            graph.AddNode(CreateAttractionId(node));
        }
        foreach (var avail in availableTransportations)
        {
            var weight = WeightFunction(avail);
            var n1 = avail.LocationIdLeft;
            var n2 = avail.LocationIdRight;
            graph.AddEdge(n1, n2, weight);
        }
        return graph;
    }
}