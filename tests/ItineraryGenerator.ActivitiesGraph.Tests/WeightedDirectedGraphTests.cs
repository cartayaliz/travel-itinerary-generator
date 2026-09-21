namespace ItineraryGenerator.ActivitiesGraph.Tests;

public class WeightedDirectedGraphTests
{
    [Fact]
    public void AddNode_ShouldAddNode()
    {
        var graph = new WeightedDirectedGraph();
        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddNode(3);

        Assert.Contains(1, graph.Nodes);
    }

    [Fact]
    public void AddArc_ShouldAddArcAndNodes()
    {
        var graph = new WeightedDirectedGraph();
        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddArc(1, 2, 10);
        Assert.Contains(1, graph.Nodes);
        Assert.Contains(2, graph.Nodes);
        Assert.True(graph.HasArc(1, 2));
        Assert.Equal(10, graph.GetWeight(1, 2));
        
        graph.AddArc(3, 4, 20.5);
        Assert.Contains(3, graph.Nodes);
        Assert.Contains(4, graph.Nodes);
        Assert.True(graph.HasArc(3, 4));
        Assert.Equal(20.5, graph.GetWeight(3, 4));
    }
}
