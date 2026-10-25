namespace Turist.Domain.DTOs;
public record ItineraryComparisonResultDto(
    IEnumerable<PackageComparisonDetailDto> PackagesCompared
);

public record PackageComparisonDetailDto(
    int PackageId,
    string PackageName,
    Dictionary<string, int> CategoryDistribution, // Ej: {"Cultura": 5, "Naturaleza": 3}
    Dictionary<string, int> EffortLevelDistribution, // Ej: {"Bajo": 2, "Medio": 4, "Alto": 2}
    bool BalanceCriteriaMet
);