using Turist.Domain.Models;
using Turist.Domain.DTOs;
using Turist.Domain.Enums;
namespace Turist.Domain.Repository;
public interface IItineraryRepository : IRepository<Itinerario>
{
    //Listado de itinerarios automáticos para un paquete específico con parámetros
    Task<IEnumerable<ItinerarySummaryDto>> GetGeneratedItinerariesByPackageAsync(int packageId);

    //Actividades más utilizadas en itinerarios finales clasificadas por esfuerzo y categoría
    Task<IEnumerable<ActivityUsageDto>> GetMostUsedActivitiesAsync(int packageId, PhysicalEffortLevel? effortLevel, ActivityCategory? category);

    // Itinerarios validados por un revisor con fecha y observaciones
    Task<IEnumerable<ItineraryValidationDto>> GetValidatedItinerariesByReviewerAsync(int reviewerId);

    //Comparación de itinerarios entre diferentes paquetes
    Task<ItineraryComparisonResultDto> CompareItinerariesAcrossPackagesAsync(IEnumerable<int> packageIds);

    //Correlación entre nivel de esfuerzo y valoración promedio de turistas
    Task<TouristItineraryCorrelationDto> GetEffortRatingCorrelationAsync(int packageId);

    //Top 10 de actividades con mayor tasa de abandono, su creador y paquete
    Task<IEnumerable<ActivityDropoutDto>> GetTopDropoutActivitiesAsync(int count = 10);
    Task<IEnumerable<ItinerarySummaryDto>> GetOtherGuidesItinerariesByPackageAsync(int packageId, int currentGuideId);
}