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
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuariosController(IUsuarioRepository usuarioRepository)
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
        if (usuario == null) return NotFound($"No se encontró el usuario con ID {id}.");

        return Ok(usuario.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UsuarioCreateDto dto)
    {
        var nuevoUsuario = dto.ToEntity();
        await _usuarioRepository.AddAsync(nuevoUsuario);

        return Ok(new { mensaje = "Usuario creado exitosamente", id = nuevoUsuario.UsuarioID });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UsuarioCreateDto dto)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
        if (usuarioExistente == null) return NotFound($"No se encontró el usuario con ID {id}.");

        dto.UpdateEntity(usuarioExistente);
        await _usuarioRepository.UpdateAsync(usuarioExistente);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return NotFound($"No se encontró el usuario con ID {id}.");

        await _usuarioRepository.DeleteAsync(usuario);
        return NoContent();
    }
}