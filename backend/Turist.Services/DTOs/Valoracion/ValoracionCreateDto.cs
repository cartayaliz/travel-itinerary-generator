namespace Turist.Services.DTOs.Valoracion;

public record ValoracionCreateDto(
    string Comentario,
    DateTime Fecha,
    int Puntuacion,
    int TuristaID,
    int? ItinerarioID, // Opcional (Arco exclusivo)
    int? VisitaID      // Opcional (Arco exclusivo)
);