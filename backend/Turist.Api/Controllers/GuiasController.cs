using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Usuario.Guia;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuiasController : ControllerBase
{
    private readonly IGuiaService _service;

    public GuiasController(IGuiaService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] GuiaCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Guía creado" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:int}/paquetes-autorizados")]
    public async Task<ActionResult> AsignarPaquetesAutorizados(int id, [FromBody] List<AsignarPaqueteAGuiaDto> dto)
    {
        try { await _service.AsignarPaquetesAutorizadosAsync(id, dto); return Ok(new { mensaje = "Paquetes autorizados asignados" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}