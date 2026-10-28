namespace Turist.Services.DTOs.Auth;

public record LoginDto(
    int UsuarioID,
    string Password
);