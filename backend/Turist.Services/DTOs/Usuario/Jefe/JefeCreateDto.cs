namespace Turist.Services.DTOs.Usuario.Jefe;

public record JefeCreateDto(
    string Nombre,
    string UserName,
    string Password
);