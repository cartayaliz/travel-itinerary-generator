namespace Turist.Domain.DTOs;
public record ItineraryValidationDto(
    Guid ItineraryId,
    string PackageName,
    string ReviewerName, // Jefe de operaciones
    DateTime ValidationDate,
    string Observations
);