namespace Turist.Api.Models;

public class Inscribir
{
    public int TuristaID { get; set; }
    public int PaqueteID { get; set; }

    // Navegación
    public Turista Turista { get; set; } = null!;
    public PaqueteTuristico Paquete { get; set; } = null!;
}