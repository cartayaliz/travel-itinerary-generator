namespace Turist.Domain.DTOs;
public record ActivityUsageDto(
    Guid ActivityId,
    string ActivityName,
    string Category,
    string EffortLevel, // Bajo, Medio, Alto
    int UsageCount
);