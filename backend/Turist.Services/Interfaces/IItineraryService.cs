using Turist.Services.DTOs.Itinerario;

namespace Turist.Services.Interfaces;

public interface IItineraryService
{
    Task AsignarTuristasAsync(int itinerarioId, List<AsignarTuristaDto> turistasDto);
}