namespace Turist.Domain.DTOs;
public record ItinerarySummaryDto(
    Guid ItineraryId,
    string PackageName,
    string CreatorName,
    DateTime CreationDate,
    Dictionary<string, string> ParametersUsed // Proporción por tipo, cobertura de categorías, total actividades
);