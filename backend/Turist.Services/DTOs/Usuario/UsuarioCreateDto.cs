using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Usuario;

public record UsuarioCreateDto(
    string Nombre,
    string Password,
    TipoUsuario TipoUsuario,
    
    // Campos opcionales (solo si es Guía)
    string? Especialidad,
    
    // Campos opcionales (solo si es Turista)
    int? Edad,
    int? GrupoID
);