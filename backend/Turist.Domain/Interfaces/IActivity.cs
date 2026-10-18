namespace Turist.Domain.Interfaces;

public interface IActivity
{
    int Id { get; }
    IGeoPoint GeoPoint { get; }
}