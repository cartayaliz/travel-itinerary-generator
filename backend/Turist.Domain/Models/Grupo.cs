namespace Turist.Domain.Models;

public class Grupo
{
    public int GrupoID { get; set; }

    // Navegación
    public List<Turista> Turistas { get; set; } = new();
}