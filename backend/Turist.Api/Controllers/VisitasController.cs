using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Visita;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitasController : ControllerBase
{
    private readonly IVisitaRepository _repository;

    public VisitasController(IVisitaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisitaResponseDto>>> GetAll()
    {
        var visitas = await _repository.GetAllAsync();
        var response = visitas.Select(v => new VisitaResponseDto(
            v.VisitaID, v.Fecha, v.Estado, v.ItinerarioID, v.TuristaID, v.ParadaID
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VisitaResponseDto>> GetById(int id)
    {
        var visita = await _repository.GetByIdAsync(id);
        if (visita == null) return NotFound($"No se encontró la visita con ID {id}.");

        return Ok(new VisitaResponseDto(
            visita.VisitaID, visita.Fecha, visita.Estado,
            visita.ItinerarioID, visita.TuristaID, visita.ParadaID
        ));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] VisitaCreateDto dto)
    {
        var visita = new Visita
        {
            Fecha = dto.Fecha,
            Estado = dto.Estado,
            ItinerarioID = dto.ItinerarioID,
            TuristaID = dto.TuristaID,
            ParadaID = dto.ParadaID
        };

        await _repository.AddAsync(visita);
        return Ok(new { mensaje = "Visita creada exitosamente", id = visita.VisitaID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VisitaCreateDto dto)
    {
        var visita = await _repository.GetByIdAsync(id);
        if (visita == null) return NotFound($"No se encontró la visita con ID {id}.");

        visita.Fecha = dto.Fecha;
        visita.Estado = dto.Estado;
        visita.ItinerarioID = dto.ItinerarioID;
        visita.TuristaID = dto.TuristaID;
        visita.ParadaID = dto.ParadaID;

        await _repository.UpdateAsync(visita);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var visita = await _repository.GetByIdAsync(id);
        if (visita == null) return NotFound($"No se encontró la visita con ID {id}.");

        await _repository.DeleteAsync(visita);
        return NoContent();
    }
}