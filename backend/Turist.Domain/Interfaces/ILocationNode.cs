namespace Turist.Domain.Interfaces;

public interface ILocationNode
{
    int Id { get; set; }
    double Latitude { get; set; }
    double Longitud { get; set; }

    public static double Distance(ILocationNode n1, ILocationNode n2)
    {
        throw new NotImplementedException();
    }
}