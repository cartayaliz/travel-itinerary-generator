namespace Turist.Api.Models;

public class Modificacion
{
    public int ModificacionId { get; set; }
    public int ItinerarioId { get; set; }
    public int TuristaId { get; set; }
    public int? GuiaId { get; set; }   // permite null
    public DateTime FechaSolicitud { get; set; }
    public DateTime? FechaRespuesta { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Turista Turista { get; set; } = null!;
    public GuiaTuristico? Guia { get; set; }
    public List<Visita> Visitas { get; set; } = new();
}