using Turist.Domain.Interfaces;
using Turist.Services;

namespace Turist.Tests.AttractionTransportWebTests;

public class ProposeAlternativeTests
{
    [Fact]
    public void ProposeAlternative_ReturnsTheBestReachableIntermediateActivity()
    {
        var web = CreateWeb(4);
        web.AddTransport(0, 1, TimeSpan.FromMinutes(5), 1m);
        web.AddTransport(1, 3, TimeSpan.FromMinutes(5), 1m);
        web.AddTransport(0, 2, TimeSpan.FromMinutes(10), 1m);
        web.AddTransport(2, 3, TimeSpan.FromMinutes(10), 1m);

        var alternative = web.ProposeAlternative(
            0,
            3,
            _ => true);

        Assert.Equal(1, alternative);
    }

    [Fact]
    public void ProposeAlternative_AppliesActivityFilter()
    {
        var web = CreateWeb(4);
        web.AddTransport(0, 1, TimeSpan.FromMinutes(5), 1m);
        web.AddTransport(1, 3, TimeSpan.FromMinutes(5), 1m);
        web.AddTransport(0, 2, TimeSpan.FromMinutes(10), 1m);
        web.AddTransport(2, 3, TimeSpan.FromMinutes(10), 1m);

        var alternative = web.ProposeAlternative(
            0,
            3,
            activity => activity == 2);

        Assert.Equal(2, alternative);
    }

    [Fact]
    public void ProposeAlternative_ReturnsNullWhenOnlyTheEndpointsAreConnected()
    {
        var web = CreateWeb(3);
        web.AddTransport(0, 2, TimeSpan.FromMinutes(5), 1m);

        var alternative = web.ProposeAlternative(
            0,
            2,
            _ => true);

        Assert.Null(alternative);
    }

    [Fact]
    public void ProposeAlternative_ReturnsNullWhenNoIntermediateActivityReachesDestination()
    {
        var web = CreateWeb(3);
        web.AddTransport(0, 1, TimeSpan.FromMinutes(5), 1m);

        var alternative = web.ProposeAlternative(
            0,
            2,
            _ => true);

        Assert.Null(alternative);
    }

    private static AttractionTransportWeb CreateWeb(int activityCount)
    {
        var activities = Enumerable.Range(0, activityCount)
            .Select(id => new TestActivity(id))
            .ToDictionary(activity => activity.Id);
        var web = new AttractionTransportWeb(
            id => activities[id],
            _ => new TestAttraction());

        foreach (var activity in activities.Values)
        {
            web.AddActivityGraph(activity.Id);
        }

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