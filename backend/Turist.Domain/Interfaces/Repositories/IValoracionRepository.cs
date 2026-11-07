using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IValoracionRepository
{
    Task<IEnumerable<Valoracion>> GetAllAsync();
    Task<Valoracion?> GetByIdAsync(int id);
    Task AddAsync(Valoracion valoracion);
    Task UpdateAsync(Valoracion valoracion);
    Task DeleteAsync(Valoracion valoracion);
}