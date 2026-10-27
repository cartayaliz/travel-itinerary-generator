using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Models;
using Turist.Domain.Repository;

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
    public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

   //Crear un Turista
    [HttpPost("turista")]
    public async Task<ActionResult> CreateTurista([FromBody] Turista turista)
    {
        await _usuarioRepository.AddAsync(turista);
        // Retornamos 201 Created enviando el objeto creado directamente
        return StatusCode(201, turista);
    }

    //Crear un Guía Turístico
    [HttpPost("guia")]
    public async Task<ActionResult> CreateGuia([FromBody] GuiaTuristico guia)
    {
        await _usuarioRepository.AddAsync(guia);
        return StatusCode(201, guia);
    }

    //Crear un Jefe de Operaciones
    [HttpPost("jefe-operaciones")]
    public async Task<ActionResult> CreateJefeDeOperaciones([FromBody] JefeOperaciones jefe)
    {
        await _usuarioRepository.AddAsync(jefe);
        return StatusCode(201, jefe);
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