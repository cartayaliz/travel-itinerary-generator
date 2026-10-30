using Turist.Domain.Models;
using Turist.Domain.Enums;
namespace Turist.Domain.Interfaces.Repositories;

public interface IActividadRepository : IRepository<Actividad>
{
    // Método específico para que el algoritmo de generación filtre 
    // directamente en la base de datos (mucho más eficiente que traer todo con GetAllAsync)
    Task<IEnumerable<Actividad>> GetParaGeneracionAsync(
        NivelEsfuerzo? esfuerzoMaximo, 
        List<TipoActividad> tiposPermitidos);
}