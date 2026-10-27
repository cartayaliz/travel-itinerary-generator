namespace Turist.Domain.Models;

public abstract class Usuario
{
    public int UsuarioID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}