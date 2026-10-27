namespace Turist.Services.DTOs;
public record PathAlternativeResultDto(
    int TargetAttractionId,
    string TargetAttractionName,
    TimeSpan EstimatedTravelTime,
    decimal TransportCost,
    IEnumerable<string> RouteSteps // Pasos de la red geográfica navegada
);