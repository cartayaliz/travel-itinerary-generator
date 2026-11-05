using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Validacion;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ValidacionService : IValidacionService
{
    private readonly IValidacionRepository _repository;

    public ValidacionService(IValidacionRepository repository)
    {
        _repository = repository;
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
}