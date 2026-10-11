namespace Turist.Domain.Interfaces;

public interface IAvailableTransportation
{
    public int LocationIdLeft { get; set; }
    public int LocationIdRight { get; set; }

    public double FastestTimeDelayAvailable { get; }
}