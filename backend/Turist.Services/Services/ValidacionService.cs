using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Validacion;
using Turist.Services.Interfaces;
using Turist.Domain.Enums;

namespace Turist.Services.Services;

public class ValidacionService : IValidacionService
{
    private readonly IValidacionRepository _repository;
    private readonly IItinerarioRepository _ItinerarioRepository;
    public ValidacionService(IValidacionRepository repository, IItinerarioRepository ItinerarioRepository)
    {
        _repository = repository;
        _ItinerarioRepository = ItinerarioRepository;
    }

    public async Task<IEnumerable<ValidacionResponseDto>> GetAllAsync()
    {
        var validaciones = await _repository.GetAllAsync();
        return validaciones.Select(v => new ValidacionResponseDto(
            v.ValidacionID, v.Estado, v.FechaValidacion, v.Observaciones, v.ItinerarioID, v.JefeID
        ));
    }

    public async Task<ValidacionResponseDto> GetByIdAsync(int id)
    {
        var validacion = await _repository.GetByIdAsync(id);
        if (validacion == null) throw new Exception("Validación no encontrada.");

        return new ValidacionResponseDto(
            validacion.ValidacionID, validacion.Estado, validacion.FechaValidacion,
            validacion.Observaciones, validacion.ItinerarioID, validacion.JefeID
        );
    }

    public async Task CreateAsync(ValidacionCreateDto dto)
    {
        var validacion = new Validacion
        {
            Estado = dto.Estado,
            FechaValidacion = dto.FechaValidacion,
            Observaciones = dto.Observaciones,
            ItinerarioID = dto.ItinerarioID,
            JefeID = dto.JefeID
        };
        await _repository.AddAsync(validacion);
    }

    public async Task UpdateAsync(int id, ValidacionCreateDto dto)
    {
        var validacion = await _repository.GetByIdAsync(id);
        if (validacion == null) throw new Exception("Validación no encontrada.");

        validacion.Estado = dto.Estado;
        validacion.FechaValidacion = dto.FechaValidacion;
        validacion.Observaciones = dto.Observaciones;
        validacion.ItinerarioID = dto.ItinerarioID;
        validacion.JefeID = dto.JefeID;

        await _repository.UpdateAsync(validacion);
    }

    public async Task DeleteAsync(int id)
    {
        var validacion = await _repository.GetByIdAsync(id);
        if (validacion == null) throw new Exception("Validación no encontrada.");
        await _repository.DeleteAsync(validacion);
    }
    public async Task ValidacionJefeDeOperaciones(int JefeID, ValidacionItinerarioDto dto)
    {
        //Obtenemos el itinerario con sus validaciones a través del repositorio
        var itinerario = await _ItinerarioRepository.GetByIdWithValidacionesAsync(dto.ItinerarioID);
        if (itinerario == null)
        {
            throw new Exception("El itinerario especificado no existe.");
        }

        //Verificamos el estado actual evaluando la última validación registrada
        var ultimaValidacion = itinerario.Validaciones
            .OrderByDescending(v => v.FechaValidacion)
            .FirstOrDefault();

        if (ultimaValidacion != null && 
            (ultimaValidacion.Estado == EstadoValidacion.Aprobado || 
            ultimaValidacion.Estado == EstadoValidacion.Rechazado))
        {
            throw new Exception($"El itinerario ya se encuentra en estado '{ultimaValidacion.Estado}' y no puede ser modificado.");
        }

        //Si pasa la regla, guardamos la nueva validación mediante su repositorio
        var nuevaValidacion = new Validacion
        {
            Estado = dto.Estado,
            Observaciones = dto.Observaciones,
            ItinerarioID = dto.ItinerarioID,
            JefeID = JefeID,
            FechaValidacion = DateTime.UtcNow
        };
        await _repository.AddAsync(nuevaValidacion);
    }
}