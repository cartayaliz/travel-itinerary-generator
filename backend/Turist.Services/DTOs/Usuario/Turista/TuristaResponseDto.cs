namespace Turist.Services.DTOs.Usuario.Turista;

public record TuristaResponseDto(
    int UsuarioID,
    string Nombre,
    string UserName,
    int Edad,
    int GrupoID
);