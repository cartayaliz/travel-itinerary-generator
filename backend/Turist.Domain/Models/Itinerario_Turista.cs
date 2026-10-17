namespace Turist.Domain.Models;

public class Itinerario_Turista
{
    public int ItinerarioID { get; set; }
    public int TuristaID { get; set; }
    public DateTime FechaAsignacion { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Turista Turista { get; set; } = null!;
}