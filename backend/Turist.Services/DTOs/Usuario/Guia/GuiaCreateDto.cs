namespace Turist.Services.DTOs.Usuario.Guia;

public record GuiaCreateDto(
    string Nombre,
    string UserName,
    string Password,
    string Especialidad
);