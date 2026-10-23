namespace Turist.Domain.Models;

public class Itinerario_Parada
{
    public int ItinerarioID { get; set; }
    public int ParadaID { get; set; }
    public int Orden { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Parada Parada { get; set; } = null!;
}