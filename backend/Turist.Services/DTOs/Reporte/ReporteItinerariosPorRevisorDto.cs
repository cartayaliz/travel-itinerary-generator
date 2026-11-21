namespace Turist.Services.DTOs.Reporte;

public record ReporteItinerariosPorRevisorDto(
    int ItinerarioID,
    string NombreItinerario,
    DateTime FechaCreacion,
    DateTime FechaValidacion,
    string Observaciones,
    string EstadoValidacion
);