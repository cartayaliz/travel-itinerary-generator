namespace Turist.Api.Models;

public class Autorizado
{
    public int GuiaID { get; set; }
    public int PaqueteID { get; set; }

    // Navegación
    public GuiaTuristico Guia { get; set; } = null!;
    public PaqueteTuristico Paquete { get; set; } = null!;
}