using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Paquete;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaquetesController : ControllerBase
{
    private readonly IPaqueteTuristicoRepository _repository;

    public PaquetesController(IPaqueteTuristicoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaqueteResponseDto>>> GetAll()
    {
        var paquetes = await _repository.GetAllAsync();

        // Mapeo directo: Convertimos las Entidades en DTOs con .Select
        var response = paquetes.Select(p => new PaqueteResponseDto(
            p.PaqueteID, p.Nombre, p.Destino
        ));

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaqueteResponseDto>> GetById(int id)
    {
        var paquete = await _repository.GetByIdAsync(id);
        if (paquete == null) return NotFound($"No se encontró el paquete con ID {id}.");

        // Mapeo directo
        return Ok(new PaqueteResponseDto(paquete.PaqueteID, paquete.Nombre, paquete.Destino));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PaqueteCreateDto dto)
    {
        // Convertimos el DTO en Entidad
        var paquete = new PaqueteTuristico
        {
            Nombre = dto.Nombre,
            Destino = dto.Destino
        };

        await _repository.AddAsync(paquete);

        return Ok(new { mensaje = "Paquete creado exitosamente", id = paquete.PaqueteID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] PaqueteCreateDto dto)
    {
        var paqueteExistente = await _repository.GetByIdAsync(id);
        if (paqueteExistente == null) return NotFound($"No se encontró el paquete con ID {id}.");

        paqueteExistente.Nombre = dto.Nombre;
        paqueteExistente.Destino = dto.Destino;

        await _repository.UpdateAsync(paqueteExistente);
        return NoContent(); // 204: Éxito, sin contenido que devolver
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var paquete = await _repository.GetByIdAsync(id);
        if (paquete == null) return NotFound($"No se encontró el paquete con ID {id}.");

        await _repository.DeleteAsync(paquete);
        return NoContent(); // 204: Éxito
    }

    //==========
    //Asignar Categorias
    //===========
    [HttpPost("{id:int}/categorias")]
    public async Task<ActionResult> AsignarCategorias(int id, [FromBody] List<AsignarCategoriaDto> categoriasDto)
    {
        var paquete = await _repository.GetByIdAsync(id);
        if (paquete == null) return NotFound($"No se encontró el paquete con ID {id}.");

        await _repository.LimpiarCategoriasAsync(id);

        var categorias = categoriasDto.Select(dto => new Paquete_Categoria
        {
            PaqueteID = id,
            CategoriaID = dto.CategoriaID
        }).ToList();

        await _repository.AsignarCategoriasAsync(categorias);

        return Ok(new { mensaje = "Categorías asignadas exitosamente" });
    }
}