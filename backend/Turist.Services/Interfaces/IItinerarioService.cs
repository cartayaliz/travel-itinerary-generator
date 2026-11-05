using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;
namespace Turist.Services.Interfaces;
public interface IItinerarioService : ICrudService<ItinerarioResponseDto, ItinerarioCreateDto>
{
    Task<Itinerario> GenerarItinerarioAutomaticoAsync(ItineraryGenerationParametersDto dto);
}