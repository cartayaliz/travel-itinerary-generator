using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IItinerarioRepository
{
    Task<IEnumerable<Itinerario>> GetAllWithParadasOrderedAsync();
}
