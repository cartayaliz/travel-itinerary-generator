namespace Turist.Services.DTOs.Usuario.Turista;

public record TuristaCreateDto(
    string Nombre,
    string UserName,
    string Password,
    int Edad,
    int GrupoID
);