using Turist.Domain.Models;
namespace Turist.Domain.Interfaces.Repositories;
public interface IItineraryGenerationStrategy
{
    Task<IEnumerable<Actividad>> GenerateAsync(ItineraryGenerationParameters parameters);
}