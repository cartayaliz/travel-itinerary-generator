using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Modificacion;
using Turist.Services.Interfaces;
using Turist.Domain.Enums;

namespace Turist.Services.Services;

public class ModificacionService : IModificacionService
{
    private readonly IModificacionRepository _repository;
    private readonly IItinerarioRepository _itinerarioRepository;

    public ModificacionService(IModificacionRepository repository, IItinerarioRepository itinerarioRepository)
    {
        _repository = repository;
        _itinerarioRepository = itinerarioRepository;
    }

    public async Task<IEnumerable<ModificacionResponseDto>> GetAllAsync()
    {
        var modificaciones = await _repository.GetAllAsync();
        return modificaciones.Select(m => new ModificacionResponseDto(
            m.ModificacionID,
            m.FechaSolicitud,
            m.FechaRespuesta,
            m.Estado,
            m.Resultado,
            m.ItinerarioID,
            m.TuristaID,
            m.GuiaID
        ));
    }

    public async Task<ModificacionResponseDto> GetByIdAsync(int id)
    {
        var modificacion = await _repository.GetByIdAsync(id);
        if (modificacion == null) throw new Exception("Modificación no encontrada.");

        return new ModificacionResponseDto(
            modificacion.ModificacionID,
            modificacion.FechaSolicitud,
            modificacion.FechaRespuesta,
            modificacion.Estado,
            modificacion.Resultado,
            modificacion.ItinerarioID,
            modificacion.TuristaID,
            modificacion.GuiaID
        );
    }

    public async Task CreateAsync(ModificacionCreateDto dto)
    {
        var modificacion = new Modificacion
        {
            FechaSolicitud = dto.FechaSolicitud,
            FechaRespuesta = dto.FechaRespuesta,
            Estado = dto.Estado,
            Resultado = dto.Resultado,
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
        modificacion.Resultado = dto.Resultado;
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
    //=============
    //Solicita una modificacion de un itinerario por un turista
    //===============
    public async Task SolicitarModificacionAsync(SolicitarModificacionDto dto)
    {
        // Validar que el itinerario existe
        var itinerario = await _itinerarioRepository.GetByIdAsync(dto.ItinerarioID);
        if (itinerario == null)
            throw new Exception("Itinerario no encontrado.");

        // Validar que el turista esté asignado al itinerario 
        var turistaAsignado = await _itinerarioRepository.IsTuristaAssignedAsync(dto.ItinerarioID, dto.TuristaID);
        if (!turistaAsignado)
            throw new Exception("El turista no está asignado a este itinerario.");

        // Crear la Modificación
        var modificacion = new Modificacion
        {
            FechaSolicitud = DateTime.UtcNow,
            FechaRespuesta = null,
            Estado = EstadoModificacion.Pendiente, // <-- Siempre empieza Pendiente
            Resultado = dto.Motivo,
            ItinerarioID = dto.ItinerarioID,
            TuristaID = dto.TuristaID,
            GuiaID = dto.GuiaID // Puede ser null si no pide apoyo
        };

        await _repository.AddAsync(modificacion);
    }
}