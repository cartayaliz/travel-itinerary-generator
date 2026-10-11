namespace Turist.Api.Models;

public class Inscribir
{
    public int TuristaId { get; set; }
    public int PaqueteId { get; set; }

    // Navegación
    public Turista Turista { get; set; } = null!;
    public PaqueteTuristico Paquete { get; set; } = null!;
}