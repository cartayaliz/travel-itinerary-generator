using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Itinerario;
using Turist.Services.Interfaces;
using Turist.Services.Mappers;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItinerariosController : ControllerBase
{
    private readonly IItinerarioService _service;

    public ItinerariosController(IItinerarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItinerarioResponseDto>>> GetAll()
    {
        try { return Ok(await _service.GetAllAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItinerarioResponseDto>> GetById(int id)
    {
        try { return Ok(await _service.GetByIdAsync(id)); }
        catch (Exception ex) { return NotFound(ex.Message); }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ItinerarioCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Itinerario creado" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ItinerarioCreateDto dto)
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
    //==============
    // Endpoint para asignar paradas
    //==============
    [HttpPost("{id:int}/paradas")]
    public async Task<ActionResult> AsignarParadas(int id, [FromBody] List<AsignarParadaAItinerarioDto> dto)
    {
        try { await _service.AsignarParadasAsync(id, dto); return Ok(new { mensaje = "Paradas asignadas" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    //==============
    // Endpoint para asignar paquetes 
    //==============
    [HttpPost("{id:int}/paquetes")]
    public async Task<ActionResult> AsignarPaquetes(int id, [FromBody] List<AsignarPaqueteAItinerarioDto> dto)
    {
        try { await _service.AsignarPaquetesAsync(id, dto); return Ok(new { mensaje = "Paquetes asignados" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    //==============
    // Endpoint para asignar turistas 
    //==============
    [HttpPost("{id:int}/turistas")]
    public async Task<ActionResult> AsignarTuristas(int id, [FromBody] List<AsignarTuristaAItinerarioDto> dto)
    {
        try
        {
            // El controlador recibe la petición HTTP y la delega al Service
            await _service.AsignarTuristasAsync(id, dto);
            return Ok(new { mensaje = "Turistas asignados exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    //==============
    // Endpoint para generar itinerarios de forma automática 
    //==============
    [HttpPost("generar-automatico")]
    public async Task<IActionResult> GenerarAutomatico([FromBody] ItineraryGenerationParametersDto dto)
    {
        try
        {
            var itinerarioCreado = await _service.GenerarItinerarioAutomaticoAsync(dto);

            var responseDto = ItinerarioMapper.ToResponseDto(itinerarioCreado);

            return Ok(responseDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}