namespace Turist.Api.Models;

public class Itinerario
{
    public int ItinerarioId { get; set; }
    public int PaqueteId { get; set; }
    public int GuiaId { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Navegación
    public PaqueteTuristico Paquete { get; set; } = null!;
    public GuiaTuristico Guia { get; set; } = null!;
    public List<Asignar> Asignaciones { get; set; } = new();
    public List<Contiene> Contiene { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
    public List<Valoracion> Valoraciones { get; set; } = new();
    public List<Modificacion> Modificaciones { get; set; } = new();
    public List<Validacion> Validaciones { get; set; } = new();
}