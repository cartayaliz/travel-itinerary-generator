using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

    private int ObtenerTuristaId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(id, out var turistaId))
            throw new UnauthorizedAccessException("No se pudo identificar al turista autenticado.");

        return turistaId;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisitaResponseDto>>> GetAll()
    {
        try
        {
            return Ok(await _service.GetAllAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VisitaResponseDto>> GetById(int id)
    {
        try
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] VisitaCreateDto dto)
    {
        try
        {
            await _service.CreateAsync(dto);
            return Ok(new { mensaje = "Visita creada" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VisitaCreateDto dto)
    {
        try
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = "Turista")]
    [HttpGet("mis-visitas")]
    public async Task<ActionResult<IEnumerable<VisitaResponseDto>>> GetMisVisitas()
    {
        try
        {
            var turistaId = ObtenerTuristaId();
            var visitas = await _service.GetByTuristaIdAsync(turistaId);

            return Ok(visitas);
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
    [HttpPost("registrar")]
    public async Task<ActionResult> RegistrarVisita([FromBody] RegistrarVisitaDto dto)
    {
        try
        {
            var turistaId = ObtenerTuristaId();

            await _service.RegistrarVisitaAsync(dto, turistaId);

            return Ok(new { mensaje = "Visita registrada exitosamente" });
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