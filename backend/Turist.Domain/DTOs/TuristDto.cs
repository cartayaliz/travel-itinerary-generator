namespace Turist.Domain.DTOs;

// DTO para la respuesta 
public record TouristResponseDto(
    int Id,
    string Name,
    int Age,
    int TravelGroupId
);

// DTO para crear un turista
public record CreateTouristDto(
    int UsuarioID,
    string Name,
    int Age,
    int TravelGroupId
);

// DTO para actualizar un turista
public record UpdateTouristDto(
    string Name,
    int Age,
    int TravelGroupId
);