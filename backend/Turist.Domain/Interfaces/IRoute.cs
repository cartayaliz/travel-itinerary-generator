namespace Turist.Domain.Interfaces;

public interface IRoute
{
    List<IVisitable> visitablesId { get; }
    DateTime StartTime { get; }
}