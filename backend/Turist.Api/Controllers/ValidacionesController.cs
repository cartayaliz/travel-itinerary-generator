using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Validacion;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValidacionesController : ControllerBase
{
    private readonly IValidacionService _service;

    public ValidacionesController(IValidacionService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ValidacionResponseDto>>> GetAll()
    {
        try { return Ok(await _service.GetAllAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ValidacionResponseDto>> GetById(int id)
    {
        try { return Ok(await _service.GetByIdAsync(id)); }
        catch (Exception ex) { return NotFound(ex.Message); }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ValidacionCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Validación creada" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ValidacionCreateDto dto)
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