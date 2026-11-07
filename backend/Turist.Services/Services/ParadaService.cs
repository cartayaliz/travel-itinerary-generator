using Turist.Domain.Interfaces.Repositories;
using Turist.Services.DTOs.Parada;
using Turist.Services.Interfaces;
using Turist.Services.Mappers;

namespace Turist.Services.Services;

public class ParadaService : IParadaService
{
    private readonly IParadaRepository _repository;

    public ParadaService(IParadaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ParadaResponseDto>> GetAllAsync()
    {
        var paradas = await _repository.GetAllAsync();
        return paradas.Select(p => p.ToDto());
    }

    public async Task<ParadaResponseDto> GetByIdAsync(int id)
    {
        var parada = await _repository.GetByIdAsync(id);
        if (parada == null) throw new Exception("Parada no encontrada.");

        return parada.ToDto();
    }

    public async Task CreateAsync(ParadaCreateDto dto)
    {
        var parada = dto.ToEntity();
        await _repository.AddAsync(parada);
    }

    public async Task UpdateAsync(int id, ParadaCreateDto dto)
    {
        var parada = await _repository.GetByIdAsync(id);
        if (parada == null) throw new Exception("Parada no encontrada.");

        dto.UpdateEntity(parada);
        await _repository.UpdateAsync(parada);
    }

    public async Task DeleteAsync(int id)
    {
        var parada = await _repository.GetByIdAsync(id);
        if (parada == null) throw new Exception("Parada no encontrada.");

        await _repository.DeleteAsync(parada);
    }
}