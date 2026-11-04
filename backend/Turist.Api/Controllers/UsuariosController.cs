using Microsoft.AspNetCore.Mvc;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Usuario.Turista;
using Turist.Services.DTOs.Usuario.Guia;
using Turist.Services.Mappers;
using Turist.Services.DTOs.Usuario; // Para UsuarioCreateDto y UsuarioResponseDto

namespace Turist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITuristaRepository _turistaRepository;
    private readonly IGuiaRepository _guiaRepository;

    public UsuariosController(
        IUsuarioRepository usuarioRepository,
        ITuristaRepository turistaRepository,
        IGuiaRepository guiaRepository)
    {
        _usuarioRepository = usuarioRepository;
        _turistaRepository = turistaRepository;
        _guiaRepository = guiaRepository;
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
    //===============
    //Asignar paquetes a turistas
    //================
    [HttpPost("{id:int}/paquetes")]
    public async Task<ActionResult> AsignarPaquetes(int id, [FromBody] List<AsignarPaqueteATuristaDto> paquetesDto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario is not Turista) return BadRequest("El usuario especificado no es un Turista.");

        await _turistaRepository.LimpiarPaquetesAsync(id);

        var paquetes = paquetesDto.Select(dto => new Turista_Paquete
        {
            TuristaID = id,
            PaqueteID = dto.PaqueteID
        }).ToList();

        await _turistaRepository.AsignarPaquetesAsync(paquetes);

        return Ok(new { mensaje = "Paquetes asignados al turista exitosamente" });
    }
    //================
    //Asignar paquetes a guias
    //================
    [HttpPost("{id:int}/paquetes-autorizados")]
    public async Task<ActionResult> AsignarPaquetesAutorizados(int id, [FromBody] List<AsignarPaqueteAGuiaDto> paquetesDto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario is not GuiaTuristico) return BadRequest("El usuario especificado no es un Guía Turístico.");

        await _guiaRepository.LimpiarPaquetesAutorizadosAsync(id);

        var paquetes = paquetesDto.Select(dto => new Guia_Paquete
        {
            GuiaID = id,
            PaqueteID = dto.PaqueteID
        }).ToList();

        await _guiaRepository.AsignarPaquetesAutorizadosAsync(paquetes);

        return Ok(new { mensaje = "Paquetes autorizados asignados al guía exitosamente" });
    }
}


