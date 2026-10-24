using Turist.Domain.Models;

namespace Turist.Api.Turist.Domain.Repository;
public interface ITouristRepository : IRepository<Turista>
{
    Task<IEnumerable<Turista>> GetTouristsByTravelGroupAsync(string travelGroupId);
    Task<bool> HasDoubleItineraryAssignmentAsync(Guid touristId, DateTime date);
}