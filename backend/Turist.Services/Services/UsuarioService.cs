using Turist.Domain.Enums;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Usuario;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();

        return usuarios.Select(u => new UsuarioResponseDto(
            u.UsuarioID,
            u.Nombre,
            // Pattern Matching para saber el rol exacto
            u is GuiaTuristico ? TipoUsuario.Guia :
            u is Turista ? TipoUsuario.Turista :
            TipoUsuario.JefeOperaciones
        ));
    }

    public async Task<UsuarioResponseDto> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) throw new Exception("Usuario no encontrado.");

        return new UsuarioResponseDto(
            usuario.UsuarioID,
            usuario.Nombre,
            usuario is GuiaTuristico ? TipoUsuario.Guia :
            usuario is Turista ? TipoUsuario.Turista :
            TipoUsuario.JefeOperaciones
        );
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) throw new Exception("Usuario no encontrado.");

        await _repository.DeleteAsync(usuario);
    }
}