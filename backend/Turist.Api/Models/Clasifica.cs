namespace Turist.Api.Models;

public class Clasifica
{
    public int PaqueteID { get; set; }
    public int CategoriaID { get; set; }

    // Navegación
    public PaqueteTuristico Paquete { get; set; } = null!;
    public Categoria Categoria { get; set; } = null!;
}