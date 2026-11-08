using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Reporte;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _service;

    public ReportesController(IReporteService service) => _service = service;

    [HttpGet("actividades-mas-usadas")]
    public async Task<ActionResult<IEnumerable<ReporteActividadesUsadasDto>>> GetActividadesMasUsadas()
    {
        try { return Ok(await _service.GetActividadesMasUsadasAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("satisfaccion-esfuerzo")]
    public async Task<ActionResult<IEnumerable<ReporteSatisfaccionEsfuerzoDto>>> GetSatisfaccionEsfuerzo()
    {
        try { return Ok(await _service.GetSatisfaccionPorEsfuerzoAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("comparacion-paquetes")]
    public async Task<ActionResult<IEnumerable<ReporteComparacionPaquetesDto>>> GetComparacionPaquetes()
    {
        try { return Ok(await _service.GetComparacionPaquetesAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }
}