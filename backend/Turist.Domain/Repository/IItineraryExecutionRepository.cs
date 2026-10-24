using Turist.Domain.Models;
using Turist.Domain.DTOs;

namespace Turist.Api.Turist.Domain.Repository;
public interface IItineraryExecutionRepository : IRepository<Itinerario_Turista>
{
    Task<ExperienceReportDto> GetExperienceReportAsync(Guid itineraryId);
    Task<IEnumerable<TouristModificationLogDto>> GetModificationLogsByTouristAsync(Guid touristId);
    
    // Comparar desempeño del turista que solicitó modificación vs promedio general de su grupo
    Task<TouristModificationComparisonDto> CompareTouristModificationVsGroupAverageAsync(Guid touristId, Guid packageId);
}