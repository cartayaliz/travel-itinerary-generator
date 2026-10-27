using Turist.Domain.Models;

namespace Turist.Services.DTOs;
public record ActivityUsageDto(
    int ActivityId,
    string ActivityName,
    Categoria Category,
    string EffortLevel, // Bajo, Medio, Alto
    int UsageCount
);