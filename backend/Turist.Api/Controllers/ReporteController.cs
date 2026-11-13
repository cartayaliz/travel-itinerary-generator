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

    [HttpGet("paquete/{paqueteId:int}/itinerarios-automaticos")]
    public async Task<ActionResult<IEnumerable<ReporteItinerariosAutomaticosPorPaqueteDto>>> GetItinerariosAutomaticosPorPaquete(int paqueteId)
    {
        try 
        { 
            return Ok(await _service.GetItinerariosAutomaticosPorPaqueteAsync(paqueteId)); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, ex.Message); 
        }
    }

    [HttpGet("actividades-mas-usadas")]
    public async Task<ActionResult<IEnumerable<ReporteActividadesUsadasDto>>> GetActividadesMasUsadas()
    {
        try { return Ok(await _service.GetActividadesMasUsadasAsync()); }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }

    [HttpGet("jefe/{jefeId:int}/itinerarios-validados")]
    public async Task<ActionResult<IEnumerable<ReporteItinerariosPorRevisorDto>>> GetItinerariosValidadosPorJefe(int jefeId)
    {
        try 
        { 
            return Ok(await _service.GetItinerariosValidadosPorJefeAsync(jefeId)); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, ex.Message); 
        }
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

    [HttpGet("correlacion-esfuerzo-valoracion")]
    public async Task<ActionResult<IEnumerable<ReporteCorrelacionEsfuerzoValoracionDto>>> GetCorrelacionEsfuerzoValoracion()
    {
        try 
        { 
            return Ok(await _service.GetCorrelacionEsfuerzoValoracionAsync()); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, ex.Message); 
        }
    }

    [HttpGet("actividades-mayor-abandono")]
    public async Task<ActionResult<IEnumerable<ReporteActividadAbandonoDto>>> GetTop10ActividadesAbandono()
    {
        try 
        { 
            return Ok(await _service.GetTop10ActividadesAbandonoAsync()); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, ex.Message); 
        }
    }
    
    [HttpGet("comparacion-modificaciones")]
    public async Task<ActionResult<IEnumerable<ReporteComparacionModificacionDto>>> GetComparacionModificacionesGrupo()
    {
        try 
        { 
            var resultado = await _service.GetComparacionModificacionesGrupoAsync();
            return Ok(resultado); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, ex.Message); 
        }
    }
}