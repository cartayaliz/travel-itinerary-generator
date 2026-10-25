using Turist.Domain.Models;

namespace Turist.Domain.Repository;
public interface ITourPackageRepository : IRepository<PaqueteTuristico>
{
    Task<IEnumerable<PaqueteTuristico>> GetPackagesByTouristAsync(int touristId);
    Task<IEnumerable<PaqueteTuristico>> GetPackagesByGuideAsync(int guideId);
    Task<bool> PackageExistsAsync(Guid packageId);
}