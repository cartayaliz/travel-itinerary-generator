namespace Turist.Api.Models;

public class Contiene
{
    public int ItinerarioId { get; set; }
    public int ActividadId { get; set; }
    public int Orden { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Actividad Actividad { get; set; } = null!;
}