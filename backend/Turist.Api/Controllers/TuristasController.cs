using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Usuario.Turista;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TuristasController : ControllerBase
{
    private readonly ITuristaService _service;

    public TuristasController(ITuristaService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TuristaCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Turista creado" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:int}/paquetes")]
    public async Task<ActionResult> AsignarPaquetes(int id, [FromBody] List<AsignarPaqueteATuristaDto> dto)
    {
        try { await _service.AsignarPaquetesAsync(id, dto); return Ok(new { mensaje = "Paquetes asignados" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}