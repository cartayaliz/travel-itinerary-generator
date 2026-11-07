using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Modificacion;

public record ModificacionResponseDto(
    int ModificacionID,
    DateTime FechaSolicitud,
    DateTime? FechaRespuesta,
    EstadoModificacion Estado,
    int ItinerarioID,
    int TuristaID,
    int? GuiaID
);