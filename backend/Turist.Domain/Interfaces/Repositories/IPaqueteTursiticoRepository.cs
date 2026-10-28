using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IPaqueteTuristicoRepository
{
    Task<IEnumerable<PaqueteTuristico>> GetAllAsync();
    Task<PaqueteTuristico?> GetByIdAsync(int id);
    Task AddAsync(PaqueteTuristico paquete);
    Task UpdateAsync(PaqueteTuristico paquete);
    Task DeleteAsync(PaqueteTuristico paquete);
}