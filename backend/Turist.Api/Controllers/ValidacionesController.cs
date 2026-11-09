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
    [HttpPost]
    [HttpPost("jefes/{jefeId}/validar")]
    public async Task<ActionResult> ValidarItinerario(int jefeId, [FromBody] ValidacionItinerarioDto request)
    {
        try
        {
            // Delegamos al servicio pasando el jefeId de la ruta y el DTO
            await _service.ValidacionJefeDeOperaciones(jefeId, request);

            return Ok(new { mensaje = "Validación registrada exitosamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}