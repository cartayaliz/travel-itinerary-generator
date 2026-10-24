namespace Turist.Domain.DTOs;
public record ExperienceReportDto(
    Guid ItineraryId,
    string PackageName,
    IEnumerable<ActivityExperienceDetailDto> ActivityDetails
);
public record ActivityExperienceDetailDto(
    string ActivityName,
    string EffortLevel,
    double AverageSatisfactionRate,
    int TotalParticipants
);