namespace Turist.Services.DTOs.Usuario.Turista;

public record TuristaCreateDto(
    string Nombre,
    string Password,
    int Edad,
    int GrupoID
);