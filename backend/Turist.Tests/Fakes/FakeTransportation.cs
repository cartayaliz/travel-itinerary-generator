using Turist.Domain.Interfaces;

namespace Turist.Tests.Fakes;

public class FakeTransportation : ITransportation
{
    public int StartVisitableId { get; init; }
    public int StopVisitableId { get; init; }
    public TimeSpan TravelTimeSpan { get; init; }
    public double Cost { get; init; }
}