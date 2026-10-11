namespace Turist.Api.Models;

public class Autorizado
{
    public int GuiaId { get; set; }
    public int PaqueteId { get; set; }

    // Navegación
    public GuiaTuristico Guia { get; set; } = null!;
    public PaqueteTuristico Paquete { get; set; } = null!;
}