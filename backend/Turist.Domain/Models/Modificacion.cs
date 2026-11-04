using Turist.Domain.Enums;
namespace Turist.Domain.Models;

public class Modificacion
{
    public int ModificacionID { get; set; }
    public DateTime FechaSolicitud { get; set; }
    public DateTime? FechaRespuesta { get; set; }
    public EstadoModificacion Estado { get; set; }
    public int ItinerarioID { get; set; }
    public int TuristaID { get; set; }
    public int? GuiaID { get; set; } // Opcional (el que apoya)

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public Turista Turista { get; set; } = null!;
    public GuiaTuristico? Guia { get; set; }
}