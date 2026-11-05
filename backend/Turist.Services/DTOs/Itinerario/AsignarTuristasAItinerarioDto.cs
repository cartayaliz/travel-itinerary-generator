namespace Turist.Services.DTOs.Itinerario;

public record AsignarTuristaAItinerarioDto(
    int TuristaID,
    DateTime FechaAsignacion
);