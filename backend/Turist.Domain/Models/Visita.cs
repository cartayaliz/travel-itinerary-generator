using Turist.Domain.Enums;
namespace Turist.Domain.Models;

public class Visita
{
    public int VisitaID { get; set; }
    public DateTime Fecha { get; set; }
    public int ItinerarioID { get; set; }
    public int TuristaID { get; set; }
    public int ParadaID { get; set; }
    public EstadoVisita Estado { get; set; }
    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Turista Turista { get; set; } = null!;
    public Parada Parada { get; set; } = null!;
    public Valoracion? Valoracion { get; set; }
}