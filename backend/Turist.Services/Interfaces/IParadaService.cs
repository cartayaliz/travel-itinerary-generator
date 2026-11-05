using Turist.Services.DTOs.Parada;

namespace Turist.Services.Interfaces;

public interface IParadaService : ICrudService<ParadaResponseDto, ParadaCreateDto>
{
    // Aquí se podria añadir métodos extra en el futuro
}