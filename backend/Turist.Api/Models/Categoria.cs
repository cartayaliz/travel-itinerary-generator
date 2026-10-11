namespace Turist.Api.Models;

public class Categoria
{
    public int CategoriaId { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Navegación
    public List<Clasifica> Clasificaciones { get; set; } = new();
    public List<Actividad> Actividades { get; set; } = new();
}