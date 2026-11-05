using Turist.Domain.Interfaces.Repositories;
using Turist.Services.DTOs.Parada;
using Turist.Services.Interfaces;
using Turist.Services.Mappers;

namespace Turist.Services.Constraints;

public class ParadaService : IParadaService
{
    private readonly IParadaRepository _paradaRepository;

    public ParadaService(IParadaRepository paradaRepository)
    {
        _paradaRepository = paradaRepository;
    }

    public async Task CrearParadaAsync(ParadaCreateDto dto)
    {
        var paradasExistentes = await _paradaRepository.GetAllAsync();
        var existeNombre = paradasExistentes.Any(p => p.Nombre.Equals(dto.Nombre, StringComparison.OrdinalIgnoreCase));

        if (existeNombre)
        {
            throw new InvalidOperationException($"Ya existe una parada registrada con el nombre '{dto.Nombre}'[cite: 2].");
        }

        var nuevaParada = dto.ToEntity();
        await _paradaRepository.AddAsync(nuevaParada);
    }
}