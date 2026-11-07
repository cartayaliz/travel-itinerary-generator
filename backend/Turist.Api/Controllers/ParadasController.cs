using Microsoft.AspNetCore.Mvc;
using Turist.Services.DTOs.Parada;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParadasController : ControllerBase
{
    private readonly IParadaService _paradaService;

    public ParadasController(IParadaService paradaService)
    {
        _paradaService = paradaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParadaResponseDto>>> GetAll()
    {
        try
        {
            return Ok(await _paradaService.GetAllAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ParadaResponseDto>> GetById(int id)
    {
        try
        {
            return Ok(await _paradaService.GetByIdAsync(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ParadaCreateDto dto)
    {
        try
        {
            await _paradaService.CreateAsync(dto);
            return Ok(new { mensaje = "Parada creada exitosamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ParadaCreateDto dto)
    {
        try
        {
            await _paradaService.UpdateAsync(id, dto);
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
            await _paradaService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}