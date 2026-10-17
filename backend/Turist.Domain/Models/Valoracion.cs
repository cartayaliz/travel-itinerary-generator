namespace Turist.Domain.Models;

public class Valoracion
{
    public int ValoracionID { get; set; }
    public int TuristaID { get; set; }
    public int ItinerarioID { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public int Puntuacion { get; set; }

    // Navegación
    public Turista Turista { get; set; } = null!;
    public Itinerario Itinerario { get; set; } = null!;
    public List<Visita> Visitas { get; set; } = new();
}