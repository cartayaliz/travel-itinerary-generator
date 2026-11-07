using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IValidacionRepository
{
    Task<IEnumerable<Validacion>> GetAllAsync();
    Task<Validacion?> GetByIdAsync(int id);
    Task AddAsync(Validacion validacion);
    Task UpdateAsync(Validacion validacion);
    Task DeleteAsync(Validacion validacion);
}