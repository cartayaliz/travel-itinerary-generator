namespace Turist.Domain.DTOs;
// Correlación entre nivel de esfuerzo físico y valoración promedio
public record TouristItineraryCorrelationDto(
    int PackageId,
    string PackageName,
    double CorrelationCoefficient,
    IEnumerable<EffortRatingPointDto> EffortRatingPoints
);

public record EffortRatingPointDto(
    string EffortLevel,
    double AverageRating
);

// Comparación del turista con modificación vs promedio general del grupo
public record TouristModificationComparisonDto(
    int TouristId,
    string TouristName,
    int PackageId,
    double TouristRatingAfterModification,
    double TravelGroupAverageRating,
    string? AdditionalGuideAssigned // Guía adicional que atendió la solicitud, si aplica
);

// Registro histórico de solicitudes de modificación virtual
public record TouristModificationLogDto(
    int RequestId,
    DateTime RequestDate,
    string RequestDetails,
    string AdditionalGuideName,
    string ModificationResultStatus
);