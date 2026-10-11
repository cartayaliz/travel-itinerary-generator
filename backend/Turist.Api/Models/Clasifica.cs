namespace Turist.Api.Models;

public class Clasifica
{
    public int PaqueteId { get; set; }
    public int CategoriaId { get; set; }

    // Navegación
    public PaqueteTuristico Paquete { get; set; } = null!;
    public Categoria Categoria { get; set; } = null!;
}