using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Categoria;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoriaResponseDto>> GetAllAsync()
    {
        var categorias = await _repository.GetAllAsync();
        return categorias.Select(c => new CategoriaResponseDto(c.CategoriaID, c.Nombre));
    }

    public async Task<CategoriaResponseDto> GetByIdAsync(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null) throw new Exception("Categoría no encontrada.");
        return new CategoriaResponseDto(categoria.CategoriaID, categoria.Nombre);
    }

    public async Task CreateAsync(CategoriaCreateDto dto)
    {
        var categoria = new Categoria { Nombre = dto.Nombre };
        await _repository.AddAsync(categoria);
    }

    public async Task UpdateAsync(int id, CategoriaCreateDto dto)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null) throw new Exception("Categoría no encontrada.");
        categoria.Nombre = dto.Nombre;
        await _repository.UpdateAsync(categoria);
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null) throw new Exception("Categoría no encontrada.");
        await _repository.DeleteAsync(categoria);
    }
}