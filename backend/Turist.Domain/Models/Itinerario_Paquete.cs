namespace Turist.Domain.Models;

public class Itinerario_Paquete
{
    public int ItinerarioID { get; set; }
    public int PaqueteID { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public PaqueteTuristico Paquete { get; set; } = null!;
}