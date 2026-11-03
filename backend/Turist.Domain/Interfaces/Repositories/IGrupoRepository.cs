using Turist.Domain.Models;
namespace Turist.Domain.Interfaces.Repositories;

public interface IGrupoRepository
{
    Task<IEnumerable<Grupo>> GetAllAsync();
    Task<Grupo?> GetByIdAsync(int id);
    Task AddAsync(Grupo grupo);
    Task UpdateAsync(Grupo grupo);
    Task DeleteAsync(Grupo grupo);
}