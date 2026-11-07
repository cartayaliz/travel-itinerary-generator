using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;

namespace Turist.Services.Interfaces;

public interface IItineraryGenerationStrategy
{
    Task<IEnumerable<Parada>> GenerateAsync(ItineraryGenerationParametersDto dto);
}