namespace Turist.Api.Models;

public class Visita
{
    public int VisitaId { get; set; }
    public int ItinerarioId { get; set; }
    public int TuristaId { get; set; }
    public int ActividadId { get; set; }
    public DateTime Fecha { get; set; }
    public int? ValidacionId { get; set; }
    public int? ModificacionId { get; set; }
    public int? ValoracionId { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Turista Turista { get; set; } = null!;
    public Actividad Actividad { get; set; } = null!;
    public Validacion? Validacion { get; set; }
    public Modificacion? Modificacion { get; set; }
    public Valoracion? Valoracion { get; set; }
}