using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IVisitaRepository
{
    Task<IEnumerable<Visita>> GetAllAsync();
    Task<Visita?> GetByIdAsync(int id);
    Task AddAsync(Visita visita);
    Task UpdateAsync(Visita visita);
    Task DeleteAsync(Visita visita);

    Task<bool> ExisteVisitaAsync(int itinerarioId, int turistaId, int paradaId);
}