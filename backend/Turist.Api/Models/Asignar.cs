namespace Turist.Api.Models;

public class Asignar
{
    public int ItinerarioId { get; set; }
    public int TuristaId { get; set; }
    public DateTime FechaAsignacion { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Turista Turista { get; set; } = null!;
}