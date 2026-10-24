using Turist.Domain.Models;

namespace Turist.Api.Turist.Domain.Repository;
public interface ITourGuideRepository : IRepository<GuiaTuristico>
{
    Task<bool> IsAuthorizedForPackageAsync(Guid guideId, Guid packageId);
    Task<IEnumerable<GuiaTuristico>> GetGuidesBySpecialtyAsync(string specialty);
}