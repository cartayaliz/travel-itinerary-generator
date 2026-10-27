using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IParadaRepository
{
    Task<IEnumerable<Parada>> GetAllAsync();
    Task<Parada?> GetByIdAsync(int id);
    Task AddAsync(Parada parada);
    Task UpdateAsync(Parada parada);
    Task DeleteAsync(Parada parada);
    Task<IEnumerable<Parada>> GetByCategoryAsync(int categoriaId);
}