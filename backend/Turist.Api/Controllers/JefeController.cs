using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Usuario.Jefe;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JefesController : ControllerBase
{
    private readonly IJefeService _service;

    public JefesController(IJefeService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] JefeCreateDto dto)
    {
        try { await _service.CreateAsync(dto); return Ok(new { mensaje = "Jefe creado" }); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}