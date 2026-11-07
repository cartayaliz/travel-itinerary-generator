using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Usuario.Turista;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class TuristaService : ITuristaService
{
    private readonly ITuristaRepository _repository;

    public TuristaService(ITuristaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TuristaResponseDto>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();

        // Filtramos solo a los que son Turistas
        var turistas = usuarios.OfType<Turista>();

        return turistas.Select(t => new TuristaResponseDto(t.UsuarioID, t.Nombre, t.Edad, t.GrupoID));
    }

    public async Task<TuristaResponseDto> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);

        // Verificamos que sea un Turista
        if (usuario is not Turista turista)
            throw new Exception("El usuario no es un Turista o no existe.");

        return new TuristaResponseDto(turista.UsuarioID, turista.Nombre, turista.Edad, turista.GrupoID);
    }

    public async Task CreateAsync(TuristaCreateDto dto)
    {
        var turista = new Turista
        {
            Nombre = dto.Nombre,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Edad = dto.Edad,
            GrupoID = dto.GrupoID
        };
        await _repository.AddAsync(turista);
    }

    public async Task UpdateAsync(int id, TuristaCreateDto dto)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not Turista turista)
            throw new Exception("El usuario no es un Turista o no existe.");

        turista.Nombre = dto.Nombre;
        if (!string.IsNullOrWhiteSpace(dto.Password))
            turista.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        turista.Edad = dto.Edad;
        turista.GrupoID = dto.GrupoID;

        await _repository.UpdateAsync(turista);
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is not Turista)
            throw new Exception("El usuario no es un Turista o no existe.");

        await _repository.DeleteAsync(usuario);
    }

    public async Task AsignarPaquetesAsync(int turistaId, List<AsignarPaqueteATuristaDto> paquetes)
    {
        await _repository.LimpiarPaquetesAsync(turistaId);

        var lista = paquetes.Select(p => new Turista_Paquete
        {
            TuristaID = turistaId,
            PaqueteID = p.PaqueteID
        }).ToList();

        await _repository.AsignarPaquetesAsync(lista);
    }
}