using Turist.Services.DTOs.Validacion;

namespace Turist.Services.Interfaces;

public interface IValidacionService : ICrudService<ValidacionResponseDto, ValidacionCreateDto>
{
    Task ValidacionJefeDeOperaciones(int jefeId, ValidacionItinerarioDto dto);
}