using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Services.DTOs.Parada;
using Turist.Services.Mappers;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParadasController : ControllerBase
{
    private readonly IParadaRepository _paradaRepository;

    public ParadasController(IParadaRepository paradaRepository)
    {
        _paradaRepository = paradaRepository;
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
        var nuevaParada = dto.ToEntity();
        await _paradaRepository.AddAsync(nuevaParada);
        
        return Ok(new { mensaje = "Parada creada exitosamente", id = nuevaParada.ParadaID });
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