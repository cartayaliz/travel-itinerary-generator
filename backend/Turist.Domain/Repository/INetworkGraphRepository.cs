using Turist.Domain.DTOs;
namespace Turist.Domain.Repository;

public interface INetworkGraphRepository
{
    // Permite recorrer la red para proponer alternativas cuando una actividad no puede realizarse a tiempo
    Task<PathAlternativeResultDto> FindAlternativeRouteAsync(int currentAttractionId, int targetAttractionId, TimeSpan maxAllowedTime);
    
    // Actualiza y recalcula dinámicamente los pesos (tiempos y costos de transporte) de la red
    Task RecalculateDynamicConnectionsAsync();
}