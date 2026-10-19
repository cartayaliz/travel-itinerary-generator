namespace Turist.Domain.Models;

public class Turista_Paquete
{
    public int TuristaID { get; set; }
    public int PaqueteID { get; set; }

    // Navegación
    public Turista Turista { get; set; } = null!;
    public PaqueteTuristico Paquete { get; set; } = null!;
}