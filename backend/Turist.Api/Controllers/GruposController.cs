using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Grupo;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GruposController : ControllerBase
{
    private readonly IGrupoRepository _repository;
    public GruposController(IGrupoRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrupoResponseDto>>> GetAll()
    {
        var grupos = await _repository.GetAllAsync();
        return Ok(grupos.Select(g => new GrupoResponseDto(g.GrupoID, g.Nombre)));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GrupoResponseDto>> GetById(int id)
    {
        var grupo = await _repository.GetByIdAsync(id);
        if (grupo == null) return NotFound();
        return Ok(new GrupoResponseDto(grupo.GrupoID, grupo.Nombre));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] GrupoCreateDto dto)
    {
        var grupo = new Grupo { Nombre = dto.Nombre };
        await _repository.AddAsync(grupo);
        return Ok(new { mensaje = "Grupo creado", id = grupo.GrupoID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] GrupoCreateDto dto)
    {
        var grupo = await _repository.GetByIdAsync(id);
        if (grupo == null) return NotFound();
        grupo.Nombre = dto.Nombre;
        await _repository.UpdateAsync(grupo);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var grupo = await _repository.GetByIdAsync(id);
        if (grupo == null) return NotFound();
        await _repository.DeleteAsync(grupo);
        return NoContent();
    }
}