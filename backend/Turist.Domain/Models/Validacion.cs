using Turist.Domain.Enums;

namespace Turist.Domain.Models;

public class Validacion
{
    public int ValidacionID { get; set; }
    public EstadoValidacion Estado { get; set; }
    public DateTime FechaValidacion { get; set; }
    public string Observaciones { get; set; } = string.Empty;

    public int ItinerarioID { get; set; }
    public int JefeID { get; set; }

    public Itinerario Itinerario { get; set; } = null!;
    public JefeOperaciones Jefe { get; set; } = null!;
}