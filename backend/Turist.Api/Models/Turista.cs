namespace Turist.Api.Models;

public class Turista
{
    public int TuristaID { get; set; }
    public int GrupoID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }

    // Navegación
    public Grupo Grupo { get; set; } = null!;
    public List<Turista_Paquete> TuristaPaquetes { get; set; } = new();
    public List<Itinerario_Turista> ItinerarioTuristas { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
    public List<Valoracion> Valoraciones { get; set; } = new();
    public List<Modificacion> Modificaciones { get; set; } = new();
}