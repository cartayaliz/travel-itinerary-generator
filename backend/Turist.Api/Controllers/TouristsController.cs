using Microsoft.AspNetCore.Mvc;
using Turist.Domain.DTOs;
using Turist.Domain.Models;
using Turist.Domain.Repository;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TouristsController : ControllerBase
{
    private readonly ITouristRepository _touristRepository;

    public TouristsController(ITouristRepository touristRepository)
    {
        _touristRepository = touristRepository;
    }

    // 1. READ: Obtener todos los turistas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TouristResponseDto>>> GetAllTourists()
    {
        var tourists = await _touristRepository.GetAllAsync();
        
        var response = tourists.Select(t => new TouristResponseDto(
            t.UsuarioID, t.Nombre, t.Edad, t.GrupoID
        ));

        return Ok(response);
    }

    // 2. READ: Obtener un turista por su ID (Guid)
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TouristResponseDto>> GetTouristById(Guid id)
    {
        var tourist = await _touristRepository.GetByIdAsync(id);

        if (tourist == null)
        {
            return NotFound($"No se encontró el turista con ID {id}.");
        }

        var response = new TouristResponseDto(
            tourist.UsuarioID, tourist.Nombre, tourist.Edad, tourist.GrupoID
        );

        return Ok(response);
    }

    // 3. CREATE: Registrar un nuevo turista
    [HttpPost]
    public async Task<ActionResult<TouristResponseDto>> CreateTourist([FromBody] CreateTouristDto dto)
    {
        var tourist = new Turista
        {
            UsuarioID = dto.UsuarioID, // Generación de Guid en el backend
            Nombre = dto.Name,
            Edad = dto.Age,
            GrupoID = dto.TravelGroupId
        };

        await _touristRepository.AddAsync(tourist);

        var response = new TouristResponseDto(
            tourist.UsuarioID, tourist.Nombre, tourist.Edad, tourist.GrupoID
        );

        return CreatedAtAction(nameof(GetTouristById), new { id = tourist.UsuarioID }, response);
    }

    // 4. UPDATE: Modificar los datos de un turista existente
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTourist(Guid id, [FromBody] UpdateTouristDto dto)
    {
        var existingTourist = await _touristRepository.GetByIdAsync(id);

        if (existingTourist == null)
        {
            return NotFound($"No se encontró el turista con ID {id}.");
        }

        // Actualizamos propiedades
        existingTourist.Nombre = dto.Name;
        existingTourist.Edad = dto.Age;
        existingTourist.GrupoID = dto.TravelGroupId;

        await _touristRepository.UpdateAsync(existingTourist);

        return NoContent(); // No Content indica éxito en la actualización
    }

    // 5. DELETE: Eliminar un turista
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTourist(Guid id)
    {
        var tourist = await _touristRepository.GetByIdAsync(id);

        if (tourist == null)
        {
            return NotFound($"No se encontró el turista con ID {id}.");
        }

        await _touristRepository.DeleteAsync(tourist);

        return NoContent();
    }
}