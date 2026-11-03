using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Modificacion;

public record ModificacionCreateDto(
    DateTime FechaSolicitud,
    DateTime? FechaRespuesta,
    EstadoModificacion Estado,
    int ItinerarioID,
    int TuristaID,
    int? GuiaID
);