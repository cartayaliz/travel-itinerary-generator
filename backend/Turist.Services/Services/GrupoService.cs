using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Grupo;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class GrupoService : IGrupoService
{
    private readonly IGrupoRepository _repository;

    public GrupoService(IGrupoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GrupoResponseDto>> GetAllAsync()
    {
        var grupos = await _repository.GetAllAsync();
        return grupos.Select(g => new GrupoResponseDto(g.GrupoID, g.Nombre));
    }

    public async Task<GrupoResponseDto> GetByIdAsync(int id)
    {
        var grupo = await _repository.GetByIdAsync(id);
        if (grupo == null) throw new Exception("Grupo no encontrado.");
        return new GrupoResponseDto(grupo.GrupoID, grupo.Nombre);
    }

    public async Task CreateAsync(GrupoCreateDto dto)
    {
        var grupo = new Grupo { Nombre = dto.Nombre };
        await _repository.AddAsync(grupo);
    }

    public async Task UpdateAsync(int id, GrupoCreateDto dto)
    {
        var grupo = await _repository.GetByIdAsync(id);
        if (grupo == null) throw new Exception("Grupo no encontrado.");

        grupo.Nombre = dto.Nombre;
        await _repository.UpdateAsync(grupo);
    }

    public async Task DeleteAsync(int id)
    {
        var grupo = await _repository.GetByIdAsync(id);
        if (grupo == null) throw new Exception("Grupo no encontrado.");

        await _repository.DeleteAsync(grupo);
    }
}