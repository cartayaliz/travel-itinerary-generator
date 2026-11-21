namespace Turist.Services.DTOs.Modificacion;

public record SolicitarModificacionDto(
    int ItinerarioID,
    int TuristaID,
    string Motivo, // El resultado o detalle de lo que pide
    int? GuiaID    // Opcional: el guía de apoyo que solicita
);