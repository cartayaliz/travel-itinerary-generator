using Turist.Services.DTOs.Usuario.Turista;

namespace Turist.Services.Interfaces;

public interface ITuristaService : ICrudService<TuristaResponseDto, TuristaCreateDto>
{
    Task AsignarPaquetesAsync(int turistaId, List<AsignarPaqueteATuristaDto> paquetes);
}