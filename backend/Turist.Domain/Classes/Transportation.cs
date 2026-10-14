using Turist.Domain.Interfaces;

namespace Turist.Domain.Classes;

public class Transportation : ITransportation
{
    public int StartLocationId { get; }
    public int StopLocationId { get; }

    public double TotalTime { get; }
    public double TotalCost { get; }

    public Transportation(int startLocId, int stopLocId, IEnumerable<IVehicle> transp)
    {
        StartLocationId = startLocId;
        StopLocationId = stopLocId;

        double accumTime = 0;
        double accumCost = 0;
        foreach (var item in transp)
        {
            accumTime += item.EstimatedDistance/ item.EstimatedSpeed;
            accumCost += item.Cost;
        }
        TotalTime = accumTime;
        TotalCost = accumCost;
    }
}