using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Usuario.Jefe;
using Turist.Services.Interfaces;
using Turist.Services.Security;

namespace Turist.Services.Services;

public class JefeService : IJefeService
{
    private readonly IUsuarioRepository _repository;

    public JefeService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<JefeResponseDto>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        var jefes = usuarios.OfType<JefeOperaciones>();
        return jefes.Select(j => new JefeResponseDto(j.UsuarioID, j.Nombre, j.UserName));
    }

    public async Task<JefeResponseDto> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not JefeOperaciones jefe) throw new Exception("Jefe no encontrado.");
        return new JefeResponseDto(jefe.UsuarioID, jefe.Nombre, jefe.UserName);
    }

    public async Task CreateAsync(JefeCreateDto dto)
    {
        var jefe = new JefeOperaciones
        {
            Nombre = dto.Nombre,
            UserName = dto.UserName,
            PasswordHash = PasswordHelper.HashPasswordCompleto(dto.Password)
        };
        await _repository.AddAsync(jefe);
    }

    public async Task UpdateAsync(int id, JefeCreateDto dto)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not JefeOperaciones jefe) throw new Exception("Jefe no encontrado.");
        jefe.Nombre = dto.Nombre;
        jefe.UserName = dto.UserName;
        if (!string.IsNullOrWhiteSpace(dto.Password))
            jefe.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        await _repository.UpdateAsync(jefe);
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not JefeOperaciones) throw new Exception("Jefe no encontrado.");
        await _repository.DeleteAsync(usuario);
    }
}