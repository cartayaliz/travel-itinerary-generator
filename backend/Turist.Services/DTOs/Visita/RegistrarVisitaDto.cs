using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Visita;

public record RegistrarVisitaDto(
    int ItinerarioID,
    int TuristaID,
    int ParadaID,
    EstadoVisita Estado
);