namespace Turist.Domain.Models;

public class JefeOperaciones : Usuario
{
    //navegacion
    public List<Validacion> Validaciones { get; set; } = new();
}