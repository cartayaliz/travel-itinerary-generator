namespace Turist.Domain.DTOs;
public record ActivityUsageDto(
    int ActivityId,
    string ActivityName,
    string Category,
    string EffortLevel, // Bajo, Medio, Alto
    int UsageCount
);