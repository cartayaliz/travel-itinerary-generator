namespace Turist.Domain.Interfaces;

public enum TransportationMedium
{
    WALKING,
    BIKE,
    VEHICLE
}

public interface ITransportation
{
    public double Distance { get; }

    public TransportationMedium Medium { get; }
}

public interface IAvailable
{
    public int LocationIdLeft { get; set; }
    public int LocationIdRight { get; set; }

    public IEnumerable<ITransportation> Transportations { get; }
}