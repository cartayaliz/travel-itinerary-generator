using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Modificacion;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ModificacionService : IModificacionService
{
    private readonly IModificacionRepository _repository;

    public ModificacionService(IModificacionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ModificacionResponseDto>> GetAllAsync()
    {
        var modificaciones = await _repository.GetAllAsync();
        return modificaciones.Select(m => new ModificacionResponseDto(
            m.ModificacionID, m.FechaSolicitud, m.FechaRespuesta, m.Estado,
            m.ItinerarioID, m.TuristaID, m.GuiaID
        ));
    }

    public async Task<ModificacionResponseDto> GetByIdAsync(int id)
    {
        var modificacion = await _repository.GetByIdAsync(id);
        if (modificacion == null) throw new Exception("Modificación no encontrada.");

        return new ModificacionResponseDto(
            modificacion.ModificacionID, modificacion.FechaSolicitud, modificacion.FechaRespuesta,
            modificacion.Estado, modificacion.ItinerarioID,
            modificacion.TuristaID, modificacion.GuiaID
        );
    }

    public async Task CreateAsync(ModificacionCreateDto dto)
    {
        var modificacion = new Modificacion
        {
            FechaSolicitud = dto.FechaSolicitud,
            FechaRespuesta = dto.FechaRespuesta,
            Estado = dto.Estado,
            ItinerarioID = dto.ItinerarioID,
            TuristaID = dto.TuristaID,
            GuiaID = dto.GuiaID
        };
        await _repository.AddAsync(modificacion);
    }

    public async Task UpdateAsync(int id, ModificacionCreateDto dto)
    {
        var modificacion = await _repository.GetByIdAsync(id);
        if (modificacion == null) throw new Exception("Modificación no encontrada.");

        modificacion.FechaSolicitud = dto.FechaSolicitud;
        modificacion.FechaRespuesta = dto.FechaRespuesta;
        modificacion.Estado = dto.Estado;
        modificacion.ItinerarioID = dto.ItinerarioID;
        modificacion.TuristaID = dto.TuristaID;
        modificacion.GuiaID = dto.GuiaID;

        await _repository.UpdateAsync(modificacion);
    }

    public async Task DeleteAsync(int id)
    {
        var modificacion = await _repository.GetByIdAsync(id);
        if (modificacion == null) throw new Exception("Modificación no encontrada.");
        await _repository.DeleteAsync(modificacion);
    }
}