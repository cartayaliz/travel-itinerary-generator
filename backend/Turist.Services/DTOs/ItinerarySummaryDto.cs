namespace Turist.Services.DTOs;
public record ItinerarySummaryDto(
    int ItineraryId,
    string PackageName,
    string CreatorName,
    DateTime CreationDate,
    Dictionary<string, string> ParametersUsed // Proporción por tipo, cobertura de categorías, total actividades
);