using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Valoracion;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValoracionesController : ControllerBase
{
    private readonly IValoracionRepository _repository;

    public ValoracionesController(IValoracionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ValoracionResponseDto>>> GetAll()
    {
        var valoraciones = await _repository.GetAllAsync();
        var response = valoraciones.Select(v => new ValoracionResponseDto(
            v.ValoracionID, v.Comentario, v.Fecha, v.Puntuacion, v.TuristaID, v.ItinerarioID, v.VisitaID
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ValoracionResponseDto>> GetById(int id)
    {
        var valoracion = await _repository.GetByIdAsync(id);
        if (valoracion == null) return NotFound($"No se encontró la valoración con ID {id}.");

        return Ok(new ValoracionResponseDto(
            valoracion.ValoracionID, valoracion.Comentario, valoracion.Fecha,
            valoracion.Puntuacion, valoracion.TuristaID, valoracion.ItinerarioID, valoracion.VisitaID
        ));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ValoracionCreateDto dto)
    {
        // Validación del Arco Exclusivo: Debe tener exactamente uno de los dos IDs
        if (dto.ItinerarioID.HasValue == dto.VisitaID.HasValue)
        {
            return BadRequest("La valoración debe estar asociada exactamente a un Itinerario o a una Visita (no a ambos, ni a ninguno).");
        }

        // Validación de Puntuación (1 a 5)
        if (dto.Puntuacion < 1 || dto.Puntuacion > 5)
        {
            return BadRequest("La puntuación debe estar entre 1 y 5.");
        }

        var valoracion = new Valoracion
        {
            Comentario = dto.Comentario,
            Fecha = dto.Fecha,
            Puntuacion = dto.Puntuacion,
            TuristaID = dto.TuristaID,
            ItinerarioID = dto.ItinerarioID,
            VisitaID = dto.VisitaID
        };

        await _repository.AddAsync(valoracion);
        return Ok(new { mensaje = "Valoración creada exitosamente", id = valoracion.ValoracionID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ValoracionCreateDto dto)
    {
        var valoracion = await _repository.GetByIdAsync(id);
        if (valoracion == null) return NotFound($"No se encontró la valoración con ID {id}.");

        if (dto.ItinerarioID.HasValue == dto.VisitaID.HasValue)
        {
            return BadRequest("La valoración debe estar asociada exactamente a un Itinerario o a una Visita.");
        }

        if (dto.Puntuacion < 1 || dto.Puntuacion > 5)
        {
            return BadRequest("La puntuación debe estar entre 1 y 5.");
        }

        valoracion.Comentario = dto.Comentario;
        valoracion.Fecha = dto.Fecha;
        valoracion.Puntuacion = dto.Puntuacion;
        valoracion.TuristaID = dto.TuristaID;
        valoracion.ItinerarioID = dto.ItinerarioID;
        valoracion.VisitaID = dto.VisitaID;

        await _repository.UpdateAsync(valoracion);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var valoracion = await _repository.GetByIdAsync(id);
        if (valoracion == null) return NotFound($"No se encontró la valoración con ID {id}.");

        await _repository.DeleteAsync(valoracion);
        return NoContent();
    }
}