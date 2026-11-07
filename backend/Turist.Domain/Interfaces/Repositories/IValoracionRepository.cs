using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IValoracionRepository
{
    Task<IEnumerable<Valoracion>> GetAllAsync();
    Task<Valoracion?> GetByIdAsync(int id);
    Task AddAsync(Valoracion valoracion);
    Task UpdateAsync(Valoracion valoracion);
    Task DeleteAsync(Valoracion valoracion);

    Task<bool> ExisteValoracionDeVisitaAsync(int turistaId, int visitaId);
    Task<bool> ExisteValoracionDeItinerarioAsync(int turistaId, int itinerarioId);
    Task<Valoracion?> GetByVisitaAndTuristaAsync(int turistaId, int visitaId);
    Task<Valoracion?> GetByItinerarioAndTuristaAsync(int turistaId, int itinerarioId);
}