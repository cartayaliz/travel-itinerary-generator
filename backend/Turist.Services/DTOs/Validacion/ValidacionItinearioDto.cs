using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Validacion;

public record ValidacionItinerarioDto(
    EstadoValidacion Estado,
    DateTime FechaValidacion,
    string Observaciones,
    int ItinerarioID
);