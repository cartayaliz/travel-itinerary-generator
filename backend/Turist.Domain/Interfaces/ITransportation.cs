namespace Turist.Domain.Interfaces;

public interface IVehicle
{
    public double EstimatedDistance {get;}
    public double EstimatedSpeed { get; }
    double Cost { get; }
}

public interface ITransportation
{
    public int StartLocationId { get; }
    public int StopLocationId { get; }
    public double TotalTime { get; }
    public double TotalCost { get; }
}