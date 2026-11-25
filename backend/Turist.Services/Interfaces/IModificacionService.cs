using Turist.Services.DTOs.Modificacion;

namespace Turist.Services.Interfaces;

public interface IModificacionService : ICrudService<ModificacionResponseDto, ModificacionCreateDto>
{

    Task<IEnumerable<ModificacionResponseDto>> GetByTuristaIdAsync(int turistaId);

    Task SolicitarModificacionAsync(SolicitarModificacionDto dto, int turistaId);
}