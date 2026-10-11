namespace Turist.Api.Models;

public class Valoracion
{
    public int ValoracionId { get; set; }
    public int TuristaId { get; set; }
    public int ItinerarioId { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public int Puntuacion { get; set; }

    // Navegación
    public Turista Turista { get; set; } = null!;
    public Itinerario Itinerario { get; set; } = null!;
    public List<Visita> Visitas { get; set; } = new();
}