using Turist.Domain.Classes;
using Turist.Domain.Interfaces;
using Turist.Services;

namespace Turist.Tests.AttractionTransportWebTests;

public class ProposeAlternativeTests
{
    [Fact]
    public void ProposeAlternatives_ReturnsTheBestNActivitiesInScoreOrder()
    {
        var web = CreateWeb(0, 1, 2, 3, 4, 5);
        web.AddConexion(0, 1, TimeSpan.FromMinutes(5), 10);
        web.AddConexion(1, 5, TimeSpan.FromMinutes(5), 10);
        web.AddConexion(0, 2, TimeSpan.FromMinutes(12), 2);
        web.AddConexion(2, 5, TimeSpan.FromMinutes(13), 3);
        web.AddConexion(0, 4, TimeSpan.FromMinutes(15), 2);
        web.AddConexion(4, 5, TimeSpan.FromMinutes(15), 3);

        var alternatives = web.ProposeAlternatives(
            2,
            0,
            5,
            TimeSpan.FromMinutes(60),
            30,
            _ => true);

        Assert.Equal([2, 4], alternatives.Select(alternative => alternative.activityId));
        Assert.Equal(
            [
                new Weight(TimeSpan.FromMinutes(25), 5),
                new Weight(TimeSpan.FromMinutes(30), 5)
            ],
            alternatives.Select(alternative => alternative.weight));
    }

    [Fact]
    public void ProposeAlternatives_FiltersByActivityIdRatherThanGraphNode()
    {
        var web = CreateWeb(101, 102, 103);
        web.AddConexion(0, 1, TimeSpan.FromMinutes(5), 1);
        web.AddConexion(1, 2, TimeSpan.FromMinutes(5), 1);

        var alternatives = web.ProposeAlternatives(
            5,
            101,
            103,
            TimeSpan.FromMinutes(30),
            10,
            activityId => activityId == 102);

        var alternative = Assert.Single(alternatives);
        Assert.Equal(102, alternative.activityId);
    }

    [Fact]
    public void ProposeAlternatives_AppliesLimitsToTheWholeRoute()
    {
        var web = CreateWeb(0, 1, 2);
        web.AddConexion(0, 1, TimeSpan.FromMinutes(6), 4);
        web.AddConexion(1, 2, TimeSpan.FromMinutes(6), 4);

        var alternatives = web.ProposeAlternatives(
            5,
            0,
            2,
            TimeSpan.FromMinutes(10),
            10,
            _ => true);

        Assert.Empty(alternatives);
    }

    [Fact]
    public void ProposeAlternatives_StillReturnsIntermediateActivitiesWhenDirectRouteExists()
    {
        var web = CreateWeb(0, 1, 2);
        web.AddConexion(0, 2, TimeSpan.FromMinutes(1), 1);
        web.AddConexion(0, 1, TimeSpan.FromMinutes(5), 2);
        web.AddConexion(1, 2, TimeSpan.FromMinutes(5), 2);

        var alternatives = web.ProposeAlternatives(
            5,
            0,
            2,
            TimeSpan.FromMinutes(30),
            10,
            _ => true);

        var alternative = Assert.Single(alternatives);
        Assert.Equal(1, alternative.activityId);
        Assert.Equal(new Weight(TimeSpan.FromMinutes(10), 4), alternative.weight);
    }

    [Fact]
    public void ProposeAlternatives_UsesTheLowestScorePathWhenSeveralPathsReachAnActivity()
    {
        var web = CreateWeb(0, 1, 2, 3, 4);
        web.AddConexion(0, 1, TimeSpan.FromMinutes(1), 10);
        web.AddConexion(1, 3, TimeSpan.FromMinutes(1), 10);
        web.AddConexion(0, 2, TimeSpan.FromMinutes(10), 1);
        web.AddConexion(2, 3, TimeSpan.FromMinutes(10), 1);
        web.AddConexion(3, 4, TimeSpan.FromMinutes(1), 1);

        var alternatives = web.ProposeAlternatives(
            1,
            0,
            4,
            TimeSpan.FromMinutes(60),
            30,
            activityId => activityId == 3);

        var alternative = Assert.Single(alternatives);
        Assert.Equal(3, alternative.activityId);
        Assert.Equal(new Weight(TimeSpan.FromMinutes(21), 3), alternative.weight);
    }

    [Fact]
    public void ProposeAlternatives_ReturnsEmptyWhenAnIntermediateCannotReachDestination()
    {
        var web = CreateWeb(0, 1, 2);
        web.AddConexion(0, 1, TimeSpan.FromMinutes(5), 1);

        var alternatives = web.ProposeAlternatives(
            5,
            0,
            2,
            TimeSpan.FromMinutes(30),
            10,
            _ => true);

        Assert.Empty(alternatives);
    }

    [Fact]
    public void ProposeAlternatives_ReturnsEmptyWhenThereAreNoIntermediateActivities()
    {
        var web = CreateWeb(0, 1);
        web.AddConexion(0, 1, TimeSpan.FromMinutes(5), 1);

        var alternatives = web.ProposeAlternatives(
            5,
            0,
            1,
            TimeSpan.FromMinutes(30),
            10,
            _ => true);

        Assert.Empty(alternatives);
    }

    [Fact]
    public void ProposeAlternatives_ReturnsEmptyWhenNumIsZero()
    {
        var web = CreateWeb(0, 1, 2);

        var alternatives = web.ProposeAlternatives(
            0,
            0,
            2,
            TimeSpan.FromMinutes(30),
            10,
            _ => true);

        Assert.Empty(alternatives);
    }

    [Fact]
    public void ProposeAlternatives_HandlesSparseNodeIdsAfterRemoval()
    {
        var web = CreateWeb(0, 1, 2);
        Assert.True(web.RemoveActivityGraph(1));
        web.AddActivityGraph(3);
        web.AddConexion(0, 3, TimeSpan.FromMinutes(5), 1);
        web.AddConexion(3, 2, TimeSpan.FromMinutes(5), 1);

        var alternatives = web.ProposeAlternatives(
            5,
            0,
            2,
            TimeSpan.FromMinutes(30),
            10,
            _ => true);

        var alternative = Assert.Single(alternatives);
        Assert.Equal(3, alternative.activityId);
    }

    [Fact]
    public void AddTransport_RejectsNegativeWeights()
    {
        var web = CreateWeb(0, 1);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => web.AddConexion(0, 1, TimeSpan.FromMinutes(-1), 1));
        Assert.Throws<ArgumentOutOfRangeException>(
            () => web.AddConexion(0, 1, TimeSpan.FromMinutes(1), -1));
    }

    private static AttractionTransportWeb CreateWeb(params int[] activityIds)
    {
        var activities = activityIds
            .Select(id => new TestActivity(id))
            .ToDictionary(activity => activity.Id);
        var web = new AttractionTransportWeb(
            id => activities[id],
            _ => new TestAttraction());
        foreach (var activityId in activityIds)
            web.AddActivityGraph(activityId);
        return web;
    }

    private sealed class TestActivity(int id) : IActivity
    {
        public int Id { get; } = id;
        public IGeoPoint GeoPoint { get; } = new TestGeoPoint();
    }

    private sealed class TestAttraction : IAttraction;

    private sealed class TestGeoPoint : IGeoPoint
    {
        public double Latitude => 0;
        public double Longitude => 0;
    }
}
