using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Usuario;

public record UsuarioResponseDto(
    int UsuarioID,
    string Nombre,
    TipoUsuario TipoUsuario
);