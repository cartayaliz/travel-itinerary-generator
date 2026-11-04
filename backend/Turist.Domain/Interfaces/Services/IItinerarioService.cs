using Turist.Domain.Models;
namespace Turist.Domain.Interfaces.Services;
public interface IItinerarioService
{
    Task<Itinerario> GenerarItinerarioAutomaticoAsync(ItineraryGenerationParameters parameters);
}