using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;
public interface IItineraryGenerationStrategy
{
    Task<Itinerario> GenerateAsync(ItineraryGenerationParameters parameters);
}