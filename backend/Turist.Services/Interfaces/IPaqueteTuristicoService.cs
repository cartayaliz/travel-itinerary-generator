using Turist.Services.DTOs.Paquete;

namespace Turist.Services.Interfaces;

public interface IPaqueteTuristicoService : ICrudService<PaqueteResponseDto, PaqueteCreateDto>
{
    Task AsignarCategoriasAsync(int paqueteId, List<int> categoriasIds);
}