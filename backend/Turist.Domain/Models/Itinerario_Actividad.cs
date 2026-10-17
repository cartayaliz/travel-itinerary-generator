namespace Turist.Domain.Models;

public class Itinerario_Actividad
{
    public int ItinerarioID { get; set; }
    public int ActividadID { get; set; }
    public int Orden { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Actividad Actividad { get; set; } = null!;
}