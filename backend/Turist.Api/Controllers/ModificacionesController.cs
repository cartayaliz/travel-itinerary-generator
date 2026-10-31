using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Modificacion;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModificacionesController : ControllerBase
{
    private readonly IModificacionRepository _repository;

    public ModificacionesController(IModificacionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModificacionResponseDto>>> GetAll()
    {
        var modificaciones = await _repository.GetAllAsync();
        var response = modificaciones.Select(m => new ModificacionResponseDto(
            m.ModificacionID, m.FechaSolicitud, m.FechaRespuesta, m.Estado,
            m.ItinerarioID, m.TuristaID, m.GuiaID
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ModificacionResponseDto>> GetById(int id)
    {
        var modificacion = await _repository.GetByIdAsync(id);
        if (modificacion == null) return NotFound($"No se encontró la modificación con ID {id}.");

        return Ok(new ModificacionResponseDto(
            modificacion.ModificacionID, modificacion.FechaSolicitud, modificacion.FechaRespuesta,
            modificacion.Estado, modificacion.ItinerarioID,
            modificacion.TuristaID, modificacion.GuiaID
        ));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ModificacionCreateDto dto)
    {
        var modificacion = new Modificacion
        {
            FechaSolicitud = dto.FechaSolicitud,
            FechaRespuesta = dto.FechaRespuesta,
            Estado = dto.Estado,
            ItinerarioID = dto.ItinerarioID,
            TuristaID = dto.TuristaID,
            GuiaID = dto.GuiaID
        };

        await _repository.AddAsync(modificacion);
        return Ok(new { mensaje = "Modificación creada exitosamente", id = modificacion.ModificacionID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ModificacionCreateDto dto)
    {
        var modificacion = await _repository.GetByIdAsync(id);
        if (modificacion == null) return NotFound($"No se encontró la modificación con ID {id}.");

        modificacion.FechaSolicitud = dto.FechaSolicitud;
        modificacion.FechaRespuesta = dto.FechaRespuesta;
        modificacion.Estado = dto.Estado;
        modificacion.ItinerarioID = dto.ItinerarioID;
        modificacion.TuristaID = dto.TuristaID;
        modificacion.GuiaID = dto.GuiaID;

        await _repository.UpdateAsync(modificacion);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var modificacion = await _repository.GetByIdAsync(id);
        if (modificacion == null) return NotFound($"No se encontró la modificación con ID {id}.");

        await _repository.DeleteAsync(modificacion);
        return NoContent();
    }
}