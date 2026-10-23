namespace Turist.Domain.Models;

public class Validacion
{
    public int ValidacionID { get; set; }
    public string Estado { get; set; } = string.Empty; // Aprobado, Rechazado
    public DateTime FechaValidacion { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public int ItinerarioID { get; set; }
    public int JefeID { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public JefeOperaciones Jefe { get; set; } = null!;
}