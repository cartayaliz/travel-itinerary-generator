namespace Turist.Services.DTOs.Usuario.Turista;

public record TuristaResponseDto(
    int UsuarioID,
    string Nombre,
    int Edad,
    int GrupoID
);