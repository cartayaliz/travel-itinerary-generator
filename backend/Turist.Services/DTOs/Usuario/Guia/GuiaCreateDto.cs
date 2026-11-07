namespace Turist.Services.DTOs.Usuario.Guia;

public record GuiaCreateDto(
    string Nombre,
    string Password,
    string Especialidad
);