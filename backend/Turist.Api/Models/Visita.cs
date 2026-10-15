namespace Turist.Api.Models;

public class Visita
{
    public int VisitaID { get; set; }
    public int ItinerarioID { get; set; }
    public int TuristaID { get; set; }
    public int ActividadID { get; set; }
    public DateTime Fecha { get; set; }
    public int? ValidacionID { get; set; }
    public int? ModificacionID { get; set; }
    public int? ValoracionID { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Turista Turista { get; set; } = null!;
    public Actividad Actividad { get; set; } = null!;
    public Validacion? Validacion { get; set; }
    public Modificacion? Modificacion { get; set; }
    public Valoracion? Valoracion { get; set; }
}