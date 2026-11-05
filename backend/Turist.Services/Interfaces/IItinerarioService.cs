using Turist.Services.DTOs.Itinerario;

namespace Turist.Services.Interfaces;

public interface IItinerarioService : ICrudService<ItinerarioResponseDto, ItinerarioCreateDto>
{
    Task AsignarParadasAsync(int itinerarioId, List<AsignarParadaAItinerarioDto> paradas);
    Task AsignarPaquetesAsync(int itinerarioId, List<AsignarPaqueteAItinerarioDto> paquetes);
    Task AsignarTuristasAsync(int itinerarioId, List<AsignarTuristaAItinerarioDto> turistas);
}