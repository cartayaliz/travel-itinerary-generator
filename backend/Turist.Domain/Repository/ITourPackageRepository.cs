using Turist.Domain.Models;

namespace Turist.Api.Turist.Domain.Repository;
public interface ITourPackageRepository : IRepository<PaqueteTuristico>
{
    Task<IEnumerable<PaqueteTuristico>> GetPackagesByTouristAsync(Guid touristId);
    Task<IEnumerable<PaqueteTuristico>> GetPackagesByGuideAsync(Guid guideId);
    Task<bool> PackageExistsAsync(Guid packageId);
}