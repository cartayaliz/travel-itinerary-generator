using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Validacion;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValidacionesController : ControllerBase
{
    private readonly IValidacionRepository _repository;

    public ValidacionesController(IValidacionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ValidacionResponseDto>>> GetAll()
    {
        var validaciones = await _repository.GetAllAsync();
        var response = validaciones.Select(v => new ValidacionResponseDto(
            v.ValidacionID, v.Estado, v.FechaValidacion,
            v.Observaciones, v.ItinerarioID, v.JefeID
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ValidacionResponseDto>> GetById(int id)
    {
        var validacion = await _repository.GetByIdAsync(id);
        if (validacion == null) return NotFound($"No se encontró la validación con ID {id}.");

        return Ok(new ValidacionResponseDto(
            validacion.ValidacionID, validacion.Estado, validacion.FechaValidacion,
            validacion.Observaciones, validacion.ItinerarioID, validacion.JefeID
        ));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ValidacionCreateDto dto)
    {
        var validacion = new Validacion
        {
            Estado = dto.Estado,
            FechaValidacion = dto.FechaValidacion,
            Observaciones = dto.Observaciones,
            ItinerarioID = dto.ItinerarioID,
            JefeID = dto.JefeID
        };

        await _repository.AddAsync(validacion);
        return Ok(new { mensaje = "Validación creada exitosamente", id = validacion.ValidacionID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ValidacionCreateDto dto)
    {
        var validacion = await _repository.GetByIdAsync(id);
        if (validacion == null) return NotFound($"No se encontró la validación con ID {id}.");

        validacion.Estado = dto.Estado;
        validacion.FechaValidacion = dto.FechaValidacion;
        validacion.Observaciones = dto.Observaciones;
        validacion.ItinerarioID = dto.ItinerarioID;
        validacion.JefeID = dto.JefeID;

        await _repository.UpdateAsync(validacion);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var validacion = await _repository.GetByIdAsync(id);
        if (validacion == null) return NotFound($"No se encontró la validación con ID {id}.");

        await _repository.DeleteAsync(validacion);
        return NoContent();
    }
}