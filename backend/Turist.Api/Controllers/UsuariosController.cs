using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Usuario;
using Turist.Services.Mappers;

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IRepository<Usuario> _usuarioRepository;

    public UsuariosController(IRepository<Usuario> usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetAll()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        var response = usuarios.Select(u => u.ToDto());
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return NotFound();

        return Ok(usuario.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UsuarioCreateDto dto)
    {
        try
        {
            var nuevoUsuario = dto.ToEntity();
            await _usuarioRepository.AddAsync(nuevoUsuario);

            return Ok(new { mensaje = "Usuario creado exitosamente", id = nuevoUsuario.UsuarioID });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = "Error al crear el usuario", detalle = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UsuarioCreateDto dto)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
        if (usuarioExistente == null) return NotFound($"No se encontró el usuario con ID {id}.");

        // Delegamos la lógica al Mapper
        dto.UpdateEntity(usuarioExistente);

        await _usuarioRepository.UpdateAsync(usuarioExistente);
        return NoContent(); // 204: Éxito
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return NotFound();

        await _usuarioRepository.DeleteAsync(usuario);
        return NoContent();
    }
}