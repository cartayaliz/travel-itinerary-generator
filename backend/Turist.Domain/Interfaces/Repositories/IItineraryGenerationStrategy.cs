using Turist.Domain.Models;
<<<<<<< HEAD
namespace Turist.Domain.Interfaces.Repositories;
public interface IItineraryGenerationStrategy
{
    Task<IEnumerable<Actividad>> GenerateAsync(ItineraryGenerationParameters parameters);
=======

namespace Turist.Domain.Interfaces.Repositories;
public interface IItineraryGenerationStrategy
{
    Task<Itinerario> GenerateAsync(ItineraryGenerationParameters parameters);
>>>>>>> develop
}