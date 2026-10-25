using Turist.Domain.Models;
namespace Turist.Domain.Repository;
public interface ITourGuideRepository : IRepository<GuiaTuristico>
{
    Task<bool> IsAuthorizedForPackageAsync(int guideId, int packageId);
    Task<IEnumerable<GuiaTuristico>> GetGuidesBySpecialtyAsync(string specialty);
}