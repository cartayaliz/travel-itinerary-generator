using Turist.Services.DTOs.Modificacion;

namespace Turist.Services.Interfaces;

public interface IModificacionService : ICrudService<ModificacionResponseDto, ModificacionCreateDto>
{
    Task SolicitarModificacionAsync(SolicitarModificacionDto dto);
}