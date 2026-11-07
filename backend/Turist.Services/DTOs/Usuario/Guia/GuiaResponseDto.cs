namespace Turist.Services.DTOs.Usuario.Guia;

public record GuiaResponseDto(
    int UsuarioID,
    string Nombre,
    string Especialidad
);