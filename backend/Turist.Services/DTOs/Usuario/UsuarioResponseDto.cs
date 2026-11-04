using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Usuario;

public record UsuarioResponseDto(
    int UsuarioID,
    string Nombre,
    TipoUsuario TipoUsuario,

    // Campos opcionales (solo si es Guía)
    string? Especialidad,

    // Campos opcionales (solo si es Turista)
    int? Edad,
    int? GrupoID
);