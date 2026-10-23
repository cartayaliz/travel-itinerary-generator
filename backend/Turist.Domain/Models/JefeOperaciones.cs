namespace Turist.Domain.Models;

public class JefeOperaciones : Usuario
{
    
    public List<Validacion> Validaciones { get; set; } = new();
}