namespace Turist.Api.Models;

public class Validacion
{
    public int ValidacionId { get; set; }
    public int ItinerarioId { get; set; }
    public int GuiaId { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaValidacion { get; set; }

    // Navegación
    public Itinerario Itinerario { get; set; } = null!;
    public GuiaTuristico Guia { get; set; } = null!;
    public List<Visita> Visitas { get; set; } = new();
}