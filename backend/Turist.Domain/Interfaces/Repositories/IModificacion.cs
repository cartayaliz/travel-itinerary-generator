using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IModificacionRepository
{
    Task<IEnumerable<Modificacion>> GetAllAsync();
    Task<Modificacion?> GetByIdAsync(int id);
    Task AddAsync(Modificacion modificacion);
    Task UpdateAsync(Modificacion modificacion);
    Task DeleteAsync(Modificacion modificacion);
}