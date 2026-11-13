namespace Turist.Services.DTOs.Reporte;

public record ReporteItinerariosAutomaticosPorPaqueteDto(
    int ItinerarioID,
    string NombreItinerario,
    DateTime FechaCreacion,
    int GuiaID,
    int? CantidadActividades,
    int? ProporcionVisitas,
    int? ProporcionExcursiones,
    int? ProporcionTalleres,
    int? CoberturaCategorias
);