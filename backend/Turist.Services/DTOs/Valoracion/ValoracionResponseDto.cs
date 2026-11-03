namespace Turist.Services.DTOs.Valoracion;

public record ValoracionResponseDto(
    int ValoracionID,
    string Comentario,
    DateTime Fecha,
    int Puntuacion,
    int TuristaID,
    int? ItinerarioID,
    int? VisitaID
);