using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IItinerarioRepository
{
    Task<IEnumerable<Itinerario>> GetAllAsync();
    Task<Itinerario?> GetByIdAsync(int id);
    Task AddAsync(Itinerario itinerario);
    Task UpdateAsync(Itinerario itinerario);
    Task DeleteAsync(Itinerario itinerario);
    
    Task LimpiarParadasAsync(int itinerarioId);
    Task AsignarParadasAsync(List<Itinerario_Parada> paradas);

    Task LimpiarPaquetesAsync(int itinerarioId);
    Task AsignarPaquetesAsync(List<Itinerario_Paquete> paquetes);
}