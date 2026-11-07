using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;
namespace Turist.Services.Interfaces;
public interface IItinerarioService : ICrudService<ItinerarioResponseDto, ItinerarioCreateDto>
{
    Task<Itinerario> GenerarItinerarioAutomaticoAsync(ItineraryGenerationParametersDto dto);
    Task AsignarPaquetesAsync(int itinerarioId, List<AsignarPaqueteAItinerarioDto> paquetes);
    Task AsignarTuristasAsync(int itinerarioId, List<AsignarTuristaAItinerarioDto> turistas);
    Task AsignarParadasAsync(int itinerarioId, List<AsignarParadaAItinerarioDto> paradas);
}