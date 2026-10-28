namespace Turist.Services.DTOs.Paquete;

public record PaqueteResponseDto(
    int PaqueteID,
    string Nombre,
    string Destino
);