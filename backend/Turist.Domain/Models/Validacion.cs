namespace Turist.Domain.Models;

public class Validacion
{
    public int ValidacionID { get; set; }
    public int ItinerarioID { get; set; }
    public int GuiaID { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaValidacion { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public GuiaTuristico Guia { get; set; } = null!;
    public List<Visita> Visitas { get; set; } = new();
}