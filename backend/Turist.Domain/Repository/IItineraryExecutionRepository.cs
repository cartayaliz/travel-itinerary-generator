using Turist.Domain.Models;
using Turist.Domain.DTOs;

namespace Turist.Domain.Repository;
public interface IItineraryExecutionRepository : IRepository<Itinerario_Turista>
{
    Task<ExperienceReportDto> GetExperienceReportAsync(int itineraryId);
    Task<IEnumerable<TouristModificationLogDto>> GetModificationLogsByTouristAsync(int touristId);
    
    // Comparar desempeño del turista que solicitó modificación vs promedio general de su grupo
    Task<TouristModificationComparisonDto> CompareTouristModificationVsGroupAverageAsync(int touristId, int packageId);
}