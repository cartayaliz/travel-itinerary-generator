using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Categoria;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repository;
    public CategoriasController(ICategoriaRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponseDto>>> GetAll()
    {
        var categorias = await _repository.GetAllAsync();
        return Ok(categorias.Select(c => new CategoriaResponseDto(c.CategoriaID, c.Nombre)));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaResponseDto>> GetById(int id)
    {
        var cat = await _repository.GetByIdAsync(id);
        if (cat == null) return NotFound();
        return Ok(new CategoriaResponseDto(cat.CategoriaID, cat.Nombre));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoriaCreateDto dto)
    {
        var categoria = new Categoria { Nombre = dto.Nombre };
        await _repository.AddAsync(categoria);
        return Ok(new { mensaje = "Categoria creada", id = categoria.CategoriaID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] CategoriaCreateDto dto)
    {
        var cat = await _repository.GetByIdAsync(id);
        if (cat == null) return NotFound();
        cat.Nombre = dto.Nombre;
        await _repository.UpdateAsync(cat);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var cat = await _repository.GetByIdAsync(id);
        if (cat == null) return NotFound();
        await _repository.DeleteAsync(cat);
        return NoContent();
    }
}