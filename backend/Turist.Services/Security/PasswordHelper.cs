using System.Security.Cryptography;
using System.Text;

namespace Turist.Services.Security;

public static class PasswordHelper
{
    // Aplica el mismo pre-hash SHA-256 que usa el frontend
    public static string GenerarSha256(string textoPlano)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(textoPlano));
        return Convert.ToHexString(bytes).ToLower();
    }

    // Encadena SHA-256 + BCrypt en un solo paso para creaciones manuales en el backend
    public static string HashPasswordCompleto(string textoPlano)
    {
        string preHash = GenerarSha256(textoPlano);
        return BCrypt.Net.BCrypt.HashPassword(preHash);
    }
}