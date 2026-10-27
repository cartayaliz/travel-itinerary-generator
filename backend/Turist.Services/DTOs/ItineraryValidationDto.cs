namespace Turist.Services.DTOs;
public record ItineraryValidationDto(
    int ItineraryId,
    string PackageName,
    string ReviewerName, // Jefe de operaciones
    DateTime ValidationDate,
    string Observations
);