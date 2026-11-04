using Turist.Domain.Enums;

namespace Turist.Services.DTOs.Itinerario;

public record ItinerarioCreateDto(
    string Nombre,
    OrigenItinerario Origen,
    int GuiaID,
    int? CantidadActividades,
    int? ProporcionVisitas,
    int? ProporcionExcursiones,
    int? ProporcionTalleres,
    int? CoberturaCategorias
);