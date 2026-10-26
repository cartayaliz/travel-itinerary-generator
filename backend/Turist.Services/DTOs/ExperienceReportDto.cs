namespace Turist.Services.DTOs;
public record ExperienceReportDto(
    int ItineraryId,
    string PackageName,
    IEnumerable<ActivityExperienceDetailDto> ActivityDetails
);
public record ActivityExperienceDetailDto(
    string ActivityName,
    string EffortLevel,
    double AverageSatisfactionRate,
    int TotalParticipants
);