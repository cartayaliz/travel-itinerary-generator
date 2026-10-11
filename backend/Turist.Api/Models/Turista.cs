namespace Turist.Api.Models;

public class Turista
{
    public int TuristaID { get; set; }
    public int GrupoID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }

    // Navegación
    public Grupo Grupo { get; set; } = null!;
    public List<Inscribir> Inscripciones { get; set; } = new();
    public List<Asignar> Asignaciones { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
    public List<Valoracion> Valoraciones { get; set; } = new();
    public List<Modificacion> Modificaciones { get; set; } = new();
}