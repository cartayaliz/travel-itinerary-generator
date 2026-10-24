using Turist.Domain.Models;
using Turist.Domain.DTOs;
using Turist.Domain.Enums;
namespace Turist.Api.Turist.Domain.Repository;
public interface IItineraryRepository : IRepository<Itinerario>
{
    //Listado de itinerarios automáticos para un paquete específico con parámetros
    Task<IEnumerable<ItinerarySummaryDto>> GetGeneratedItinerariesByPackageAsync(Guid packageId);

    //Actividades más utilizadas en itinerarios finales clasificadas por esfuerzo y categoría
    Task<IEnumerable<ActivityUsageDto>> GetMostUsedActivitiesAsync(Guid packageId, PhysicalEffortLevel? effortLevel, ActivityCategory? category);

    // Itinerarios validados por un revisor con fecha y observaciones
    Task<IEnumerable<ItineraryValidationDto>> GetValidatedItinerariesByReviewerAsync(Guid reviewerId);

    //Comparación de itinerarios entre diferentes paquetes
    Task<ItineraryComparisonResultDto> CompareItinerariesAcrossPackagesAsync(IEnumerable<Guid> packageIds);

    //Correlación entre nivel de esfuerzo y valoración promedio de turistas
    Task<TouristItineraryCorrelationDto> GetEffortRatingCorrelationAsync(Guid packageId);

    //Top 10 de actividades con mayor tasa de abandono, su creador y paquete
    Task<IEnumerable<ActivityDropoutDto>> GetTopDropoutActivitiesAsync(int count = 10);
    Task<IEnumerable<ItinerarySummaryDto>> GetOtherGuidesItinerariesByPackageAsync(Guid packageId, Guid currentGuideId);
}