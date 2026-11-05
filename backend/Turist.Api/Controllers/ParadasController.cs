using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Services.DTOs.Parada;
using Turist.Services.Mappers;
using Turist.Services.Interfaces;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParadasController : ControllerBase
{
    private readonly IParadaRepository _paradaRepository;
    private readonly IParadaService _paradaService;

    public ParadasController(IParadaRepository paradaRepository, IParadaService paradaService)
    {
        _paradaRepository = paradaRepository;
        _paradaService = paradaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParadaResponseDto>>> GetAll()
    {
        var paradas = await _paradaRepository.GetAllAsync();
        var response = paradas.Select(p => p.ToDto());
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ParadaResponseDto>> GetById(int id)
    {
        var parada = await _paradaRepository.GetByIdAsync(id);
        if (parada == null) return NotFound($"No se encontró la parada con ID {id}.");

        return Ok(parada.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ParadaCreateDto dto)
    {
        try
        {
            await _paradaService.CrearParadaAsync(dto);
            return StatusCode(201, new { mensaje = "Parada creada exitosamente" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ParadaCreateDto dto)
    {
        var paradaExistente = await _paradaRepository.GetByIdAsync(id);
        if (paradaExistente == null) return NotFound($"No se encontró la parada con ID {id}.");

        // El Mapper hace la magia
        dto.UpdateEntity(paradaExistente);

        await _paradaRepository.UpdateAsync(paradaExistente);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var parada = await _paradaRepository.GetByIdAsync(id);
        if (parada == null) return NotFound($"No se encontró la parada con ID {id}.");

        await _paradaRepository.DeleteAsync(parada);
        return NoContent();
    }
}