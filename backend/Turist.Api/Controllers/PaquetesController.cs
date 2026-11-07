using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Paquete;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaquetesController : ControllerBase
{
    private readonly IPaqueteTuristicoService _service;

    public PaquetesController(IPaqueteTuristicoService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaqueteResponseDto>>> GetAll()
    {
        try { return Ok(await _service.GetAllAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaqueteResponseDto>> GetById(int id)
    {
        try { return Ok(await _service.GetByIdAsync(id)); }
        catch (Exception ex) { return NotFound(ex.Message); }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PaqueteCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Paquete creado" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] PaqueteCreateDto dto)
    {
        try { await _service.UpdateAsync(id, dto); return NoContent(); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try { await _service.DeleteAsync(id); return NoContent(); }
        catch (Exception ex) { return NotFound(ex.Message); }
    }

    [HttpPost("{id:int}/categorias")]
    public async Task<ActionResult> AsignarCategorias(int id, [FromBody] List<AsignarCategoriaDto> dto)
    {
        try
        {
            await _service.AsignarCategoriasAsync(id, dto.Select(d => d.CategoriaID).ToList());
            return Ok(new { mensaje = "Categorías asignadas" });
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}