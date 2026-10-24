public record PathAlternativeResultDto(
    Guid TargetAttractionId,
    string TargetAttractionName,
    TimeSpan EstimatedTravelTime,
    decimal TransportCost,
    IEnumerable<string> RouteSteps // Pasos de la red geográfica navegada
);