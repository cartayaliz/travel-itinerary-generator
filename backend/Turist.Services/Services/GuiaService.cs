using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Usuario.Guia;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class GuiaService : IGuiaService
{
    private readonly IGuiaRepository _repository;

    public GuiaService(IGuiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GuiaResponseDto>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();

        // Filtramos solo a los Guías
        var guias = usuarios.OfType<GuiaTuristico>();

        return guias.Select(g => new GuiaResponseDto(g.UsuarioID, g.Nombre, g.Especialidad));
    }

    public async Task<GuiaResponseDto> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not GuiaTuristico guia)
            throw new Exception("El usuario no es un Guía o no existe.");

        return new GuiaResponseDto(guia.UsuarioID, guia.Nombre, guia.Especialidad);
    }

    public async Task CreateAsync(GuiaCreateDto dto)
    {
        var guia = new GuiaTuristico
        {
            Nombre = dto.Nombre,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Especialidad = dto.Especialidad
        };
        await _repository.AddAsync(guia);
    }

    public async Task UpdateAsync(int id, GuiaCreateDto dto)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not GuiaTuristico guia)
            throw new Exception("El usuario no es un Guía o no existe.");

        guia.Nombre = dto.Nombre;
        if (!string.IsNullOrWhiteSpace(dto.Password))
            guia.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        guia.Especialidad = dto.Especialidad;

        await _repository.UpdateAsync(guia);
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not GuiaTuristico)
            throw new Exception("El usuario no es un Guía o no existe.");

        await _repository.DeleteAsync(usuario);
    }

    public async Task AsignarPaquetesAutorizadosAsync(int guiaId, List<AsignarPaqueteAGuiaDto> paquetes)
    {
        await _repository.LimpiarPaquetesAutorizadosAsync(guiaId);

        var lista = paquetes.Select(p => new Guia_Paquete
        {
            GuiaID = guiaId,
            PaqueteID = p.PaqueteID
        }).ToList();

        await _repository.AsignarPaquetesAutorizadosAsync(lista);
    }
}