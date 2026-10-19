namespace Turist.Domain.Models;

public class Guia_Paquete
{
    public int GuiaID { get; set; }
    public int PaqueteID { get; set; }

    // Navegación
    public GuiaTuristico Guia { get; set; } = null!;
    public PaqueteTuristico Paquete { get; set; } = null!;
}