namespace Turist.Domain.Models;

public class Categoria
{
    public int CategoriaID { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Navegación
    public List<Paquete_Categoria> PaqueteCategorias { get; set; } = new();
    public List<Parada> Paradas { get; set; } = new();
}