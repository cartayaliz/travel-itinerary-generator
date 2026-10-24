using Turist.Domain.Models;
using Turist.Domain.DTOs;
using Turist.Domain.Enums;
namespace Turist.Api.Turist.Domain.Repository;
public interface IActivityRepository : IRepository<Actividad>
{
    Task<IEnumerable<Actividad>> GetByCreatorAsync(Guid guideId);
    Task<IEnumerable<Actividad>> GetByFiltersAsync(ActivityCategory category, ActivityType type, PhysicalEffortLevel effortLevel);
    Task<bool> ExistsByNameAsync(string name);
}