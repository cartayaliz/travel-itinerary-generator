namespace Turist.Services.DTOs.Itinerario;

// Representa una parada con su número de orden
public record AsignarParadaDto(
    int ParadaID,
    int Orden
);