using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Paquete;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class PaqueteTuristicoService : IPaqueteTuristicoService
{
    private readonly IPaqueteTuristicoRepository _repository;

    public PaqueteTuristicoService(IPaqueteTuristicoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PaqueteResponseDto>> GetAllAsync()
    {
        var paquetes = await _repository.GetAllAsync();
        return paquetes.Select(p => new PaqueteResponseDto(p.PaqueteID, p.Nombre, p.Destino));
    }

    public async Task<PaqueteResponseDto> GetByIdAsync(int id)
    {
        var paquete = await _repository.GetByIdAsync(id);
        if (paquete == null) throw new Exception("Paquete no encontrado.");
        return new PaqueteResponseDto(paquete.PaqueteID, paquete.Nombre, paquete.Destino);
    }

    public async Task CreateAsync(PaqueteCreateDto dto)
    {
        var paquete = new PaqueteTuristico { Nombre = dto.Nombre, Destino = dto.Destino };
        await _repository.AddAsync(paquete);
    }

    public async Task UpdateAsync(int id, PaqueteCreateDto dto)
    {
        var paquete = await _repository.GetByIdAsync(id);
        if (paquete == null) throw new Exception("Paquete no encontrado.");
        paquete.Nombre = dto.Nombre;
        paquete.Destino = dto.Destino;
        await _repository.UpdateAsync(paquete);
    }

    public async Task DeleteAsync(int id)
    {
        var paquete = await _repository.GetByIdAsync(id);
        if (paquete == null) throw new Exception("Paquete no encontrado.");
        await _repository.DeleteAsync(paquete);
    }

    public async Task AsignarCategoriasAsync(int paqueteId, List<int> categoriasIds)
    {
        await _repository.LimpiarCategoriasAsync(paqueteId);
        var relaciones = categoriasIds.Select(catId => new Paquete_Categoria
        {
            PaqueteID = paqueteId,
            CategoriaID = catId
        }).ToList();
        await _repository.AsignarCategoriasAsync(relaciones);
    }
}