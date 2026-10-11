namespace Turist.Api.Models;

public class Grupo
{
    public int GrupoId { get; set; }

    // Navegación
    public List<Turista> Turistas { get; set; } = new();
}