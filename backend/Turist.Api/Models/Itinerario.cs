namespace Turist.Api.Models;

public class Itinerario
{
    public int ItinerarioID { get; set; }
    public int PaqueteID { get; set; }
    public int GuiaID { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Navegación
    public PaqueteTuristico Paquete { get; set; } = null!;
    public GuiaTuristico Guia { get; set; } = null!;
    public List<Itinerario_Turista> ItinerarioTuristas { get; set; } = new();
    public List<Itinerario_Actividad> ItinerarioActividades { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
    public List<Valoracion> Valoraciones { get; set; } = new();
    public List<Modificacion> Modificaciones { get; set; } = new();
    public List<Validacion> Validaciones { get; set; } = new();
}