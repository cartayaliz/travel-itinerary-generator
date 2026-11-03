using Turist.Domain.Models;
using Turist.Domain.Enums;
namespace Turist.Domain.Interfaces.Repositories;

public interface IActividadRepository
{
    Task<Actividad?> GetByIdAsync(int id);
    Task<IEnumerable<Actividad>> GetAllAsync();
    Task AddAsync(Actividad entity);
    Task UpdateAsync(Actividad entity);
    Task DeleteAsync(Actividad entity);
    Task<IEnumerable<Actividad>> GetParaGeneracionAsync(
        NivelEsfuerzo? esfuerzoMaximo, 
        List<TipoActividad> tiposPermitidos);
}