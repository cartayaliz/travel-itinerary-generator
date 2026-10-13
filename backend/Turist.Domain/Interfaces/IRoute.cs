namespace Turist.Domain.Interfaces;

public interface IRoute
{
    List<IVisitableLocation> Steps { get; }
    DateTime StartTime { get; }
}