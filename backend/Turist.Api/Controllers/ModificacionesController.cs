using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Modificacion;
using Turist.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModificacionesController : ControllerBase
{
    private readonly IModificacionService _service;

    public ModificacionesController(IModificacionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModificacionResponseDto>>> GetAll()
    {
        try { return Ok(await _service.GetAllAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ModificacionResponseDto>> GetById(int id)
    {
        try { return Ok(await _service.GetByIdAsync(id)); }
        catch (Exception ex) { return NotFound(ex.Message); }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ModificacionCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Modificación creada" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ModificacionCreateDto dto)
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

    private int ObtenerTuristaId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(id, out var turistaId))
            throw new UnauthorizedAccessException("No se pudo identificar al turista autenticado.");

        return turistaId;
    }

    [Authorize(Roles = "Turista")]
    [HttpGet("mis-solicitudes")]
    public async Task<ActionResult<IEnumerable<ModificacionResponseDto>>> GetMisSolicitudes()
    {
        try
        {
            var turistaId = ObtenerTuristaId();
            var solicitudes = await _service.GetByTuristaIdAsync(turistaId);

            return Ok(solicitudes);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = ex.Message });
        }
    }

    [Authorize(Roles = "Turista")]
    [HttpPost("solicitar")]
    public async Task<ActionResult> Solicitar([FromBody] SolicitarModificacionDto dto)
    {
        try
        {
            var turistaId = ObtenerTuristaId();

            await _service.SolicitarModificacionAsync(dto, turistaId);

            return Ok(new { mensaje = "Solicitud registrada exitosamente" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}