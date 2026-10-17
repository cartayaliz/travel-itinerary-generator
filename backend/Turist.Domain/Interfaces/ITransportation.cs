namespace Turist.Domain.Interfaces;

public interface ITransportation
{
    public int StartVisitableId { get; }
    public int StopVisitableId { get; }
    public TimeSpan TravelTimeSpan { get; }
    public double Cost { get; }
}