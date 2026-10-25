using Turist.Domain.Models;

namespace Turist.Domain.Repository;
public interface ITouristRepository : IRepository<Turista>
{
    Task<IEnumerable<Turista>> GetTouristsByTravelGroupAsync(int travelGroupId);
    Task<bool> HasDoubleItineraryAssignmentAsync(int touristId, DateTime date);
}