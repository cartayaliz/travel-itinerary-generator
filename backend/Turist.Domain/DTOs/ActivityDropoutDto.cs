namespace Turist.Domain.DTOs;
public record ActivityDropoutDto(
    Guid ActivityId,
    string ActivityName,
    string CreatorGuideName,
    string TourPackageName,
    double DropoutRate // Porcentaje o tasa de abandono
);