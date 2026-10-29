using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Itinerario;

public record ItinerarioResponseDto(
    int ItinerarioID,
    string Nombre,
    OrigenItinerario Origen,
    DateTime FechaCreacion,
    int GuiaID,
    int? CantidadActividades,
    int? ProporcionVisitas,
    int? ProporcionExcursiones,
    int? ProporcionTalleres,
    int? CoberturaCategorias
);