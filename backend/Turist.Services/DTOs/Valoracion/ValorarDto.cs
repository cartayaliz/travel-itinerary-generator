namespace Turist.Services.DTOs.Valoracion;

public record ValorarDto(
    int TuristaID,
    int? ItinerarioID,
    int? VisitaID,
    int Puntuacion,
    string Comentario
);