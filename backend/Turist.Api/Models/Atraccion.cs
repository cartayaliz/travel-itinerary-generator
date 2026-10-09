namespace Turist.Api.Models;

public class Atraccion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public string Categoria { get; set; } = "";
    public string NivelEsfuerzo { get; set; } = "";
}
