using Turist.Services.DTOs.Usuario.Guia;

namespace Turist.Services.Interfaces;

public interface IGuiaService : ICrudService<GuiaResponseDto, GuiaCreateDto>
{
    Task AsignarPaquetesAutorizadosAsync(int guiaId, List<AsignarPaqueteAGuiaDto> paquetes);
}