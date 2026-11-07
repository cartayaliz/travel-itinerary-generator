using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Visita;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitasController : ControllerBase
{
    private readonly IVisitaService _service;

    public VisitasController(IVisitaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisitaResponseDto>>> GetAll()
    {
        try { return Ok(await _service.GetAllAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VisitaResponseDto>> GetById(int id)
    {
        try { return Ok(await _service.GetByIdAsync(id)); }
        catch (Exception ex) { return NotFound(ex.Message); }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] VisitaCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Visita creada" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VisitaCreateDto dto)
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
}