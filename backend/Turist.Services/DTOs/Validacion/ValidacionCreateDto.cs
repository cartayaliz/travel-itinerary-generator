using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Validacion;

public record ValidacionCreateDto(
    EstadoValidacion Estado,
    DateTime FechaValidacion,
    string Observaciones,
    int ItinerarioID,
    int JefeID
);