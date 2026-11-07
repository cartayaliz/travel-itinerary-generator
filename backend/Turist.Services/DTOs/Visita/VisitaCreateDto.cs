using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Visita;

public record VisitaCreateDto(
    DateTime Fecha,
    EstadoVisita Estado,
    int ItinerarioID,
    int TuristaID,
    int ParadaID
);