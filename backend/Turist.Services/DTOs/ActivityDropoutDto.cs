namespace Turist.Services.DTOs;
public record ActivityDropoutDto(
    int ActivityId,
    string ActivityName,
    string CreatorGuideName,
    string TourPackageName,
    double DropoutRate // tasa de abandono
);