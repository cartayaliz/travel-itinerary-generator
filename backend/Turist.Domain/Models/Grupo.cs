namespace Turist.Domain.Models;

public class Grupo
{

    public int GrupoID { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Navegación
    public List<Turista> Turistas { get; set; } = new();
}