using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Visita;

public record VisitaResponseDto(
    int VisitaID,
    DateTime Fecha,
    EstadoVisita Estado,
    int ItinerarioID,
    int TuristaID,
    int ParadaID
);