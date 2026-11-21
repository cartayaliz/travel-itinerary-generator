using Turist.Services.DTOs.Valoracion;

namespace Turist.Services.Interfaces;

public interface IValoracionService : ICrudService<ValoracionResponseDto, ValoracionCreateDto>
{
    Task ValorarAsync(ValorarDto dto);
}