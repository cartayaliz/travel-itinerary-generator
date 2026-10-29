using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItinerariosController : ControllerBase
{
    private readonly IItinerarioRepository _repository;

    public ItinerariosController(IItinerarioRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ItinerarioCreateDto dto)
    {
        var itinerario = new Itinerario
        {
            Nombre = dto.Nombre,
            Origen = dto.Origen,
            GuiaID = dto.GuiaID,
            FechaCreacion = DateTime.UtcNow,
            CantidadActividades = dto.CantidadActividades,
            ProporcionVisitas = dto.ProporcionVisitas,
            ProporcionExcursiones = dto.ProporcionExcursiones,
            ProporcionTalleres = dto.ProporcionTalleres,
            CoberturaCategorias = dto.CoberturaCategorias
        };

        await _repository.AddAsync(itinerario);

        return Ok(new { mensaje = "Itinerario creado", id = itinerario.ItinerarioID });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItinerarioResponseDto>>> GetAll()
    {
        var itinerarios = await _repository.GetAllAsync();
        var response = itinerarios.Select(i => new ItinerarioResponseDto(
            i.ItinerarioID, i.Nombre, i.Origen, i.FechaCreacion, i.GuiaID,
            i.CantidadActividades, i.ProporcionVisitas, i.ProporcionExcursiones,
            i.ProporcionTalleres, i.CoberturaCategorias
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItinerarioResponseDto>> GetById(int id)
    {
        var itinerario = await _repository.GetByIdAsync(id);
        if (itinerario == null) return NotFound($"No se encontró el itinerario con ID {id}.");

        return Ok(new ItinerarioResponseDto(
            itinerario.ItinerarioID, itinerario.Nombre, itinerario.Origen,
            itinerario.FechaCreacion, itinerario.GuiaID,
            itinerario.CantidadActividades, itinerario.ProporcionVisitas,
            itinerario.ProporcionExcursiones, itinerario.ProporcionTalleres,
            itinerario.CoberturaCategorias
        ));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ItinerarioCreateDto dto)
    {
        var itinerarioExistente = await _repository.GetByIdAsync(id);
        if (itinerarioExistente == null) return NotFound($"No se encontró el itinerario con ID {id}.");

        itinerarioExistente.Nombre = dto.Nombre;
        itinerarioExistente.Origen = dto.Origen;
        itinerarioExistente.GuiaID = dto.GuiaID;
        itinerarioExistente.CantidadActividades = dto.CantidadActividades;
        itinerarioExistente.ProporcionVisitas = dto.ProporcionVisitas;
        itinerarioExistente.ProporcionExcursiones = dto.ProporcionExcursiones;
        itinerarioExistente.ProporcionTalleres = dto.ProporcionTalleres;
        itinerarioExistente.CoberturaCategorias = dto.CoberturaCategorias;

        await _repository.UpdateAsync(itinerarioExistente);
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var itinerario = await _repository.GetByIdAsync(id);
        if (itinerario == null) return NotFound($"No se encontró el itinerario con ID {id}.");

        await _repository.DeleteAsync(itinerario);
        return NoContent();
    }
    // Endpoint para asignar (o actualizar) las paradas de un itinerario
    [HttpPost("{id:int}/paradas")]
    public async Task<ActionResult> AsignarParadas(int id, [FromBody] List<AsignarParadaDto> paradasDto)
    {
        var itinerario = await _repository.GetByIdAsync(id);
        if (itinerario == null) return NotFound($"No se encontró el itinerario con ID {id}.");

        if (paradasDto.GroupBy(p => p.Orden).Any(g => g.Count() > 1))
        {
            return BadRequest("No se puede asignar el mismo número de orden a dos paradas.");
        }

        await _repository.LimpiarParadasAsync(id);


        var paradas = paradasDto.Select(dto => new Itinerario_Parada
        {
            ItinerarioID = id,
            ParadaID = dto.ParadaID,
            Orden = dto.Orden
        }).ToList();

        await _repository.AsignarParadasAsync(paradas);

        return Ok(new { mensaje = "Paradas asignadas exitosamente" });
    }
    // Endpoint para asignar itinerarios a paquetes
    [HttpPost("{id:int}/paquetes")]
    public async Task<ActionResult> AsignarPaquetes(int id, [FromBody] List<AsignarPaqueteDto> paquetesDto)
    {
        var itinerario = await _repository.GetByIdAsync(id);
        if (itinerario == null) return NotFound($"No se encontró el itinerario con ID {id}.");

        await _repository.LimpiarPaquetesAsync(id);

        var paquetes = paquetesDto.Select(dto => new Itinerario_Paquete
        {
            ItinerarioID = id,
            PaqueteID = dto.PaqueteID
        }).ToList();

        await _repository.AsignarPaquetesAsync(paquetes);

        return Ok(new { mensaje = "Paquetes asignados exitosamente" });
    }
}