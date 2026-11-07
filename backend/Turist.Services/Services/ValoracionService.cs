using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Valoracion;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ValoracionService : IValoracionService
{
    private readonly IValoracionRepository _repository;

    public ValoracionService(IValoracionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ValoracionResponseDto>> GetAllAsync()
    {
        var valoraciones = await _repository.GetAllAsync();
        return valoraciones.Select(v => new ValoracionResponseDto(
            v.ValoracionID, v.Comentario, v.Fecha, v.Puntuacion, v.TuristaID, v.ItinerarioID, v.VisitaID
        ));
    }

    public async Task<ValoracionResponseDto> GetByIdAsync(int id)
    {
        var valoracion = await _repository.GetByIdAsync(id);
        if (valoracion == null) throw new Exception("Valoración no encontrada.");

        return new ValoracionResponseDto(
            valoracion.ValoracionID, valoracion.Comentario, valoracion.Fecha,
            valoracion.Puntuacion, valoracion.TuristaID, valoracion.ItinerarioID, valoracion.VisitaID
        );
    }

    public async Task CreateAsync(ValoracionCreateDto dto)
    {
        // Validación del Arco Exclusivo (Regla de Negocio)
        if (dto.ItinerarioID.HasValue == dto.VisitaID.HasValue)
            throw new Exception("La valoración debe estar asociada a un Itinerario o a una Visita (no a ambos).");

        if (dto.Puntuacion < 1 || dto.Puntuacion > 5)
            throw new Exception("La puntuación debe estar entre 1 y 5.");

        var valoracion = new Valoracion
        {
            Comentario = dto.Comentario,
            Fecha = dto.Fecha,
            Puntuacion = dto.Puntuacion,
            TuristaID = dto.TuristaID,
            ItinerarioID = dto.ItinerarioID,
            VisitaID = dto.VisitaID
        };
        await _repository.AddAsync(valoracion);
    }

    public async Task UpdateAsync(int id, ValoracionCreateDto dto)
    {
        var valoracion = await _repository.GetByIdAsync(id);
        if (valoracion == null) throw new Exception("Valoración no encontrada.");

        // Mismas validaciones que en el Create
        if (dto.ItinerarioID.HasValue == dto.VisitaID.HasValue)
            throw new Exception("La valoración debe estar asociada a un Itinerario o a una Visita.");

        if (dto.Puntuacion < 1 || dto.Puntuacion > 5)
            throw new Exception("La puntuación debe estar entre 1 y 5.");

        valoracion.Comentario = dto.Comentario;
        valoracion.Fecha = dto.Fecha;
        valoracion.Puntuacion = dto.Puntuacion;
        valoracion.TuristaID = dto.TuristaID;
        valoracion.ItinerarioID = dto.ItinerarioID;
        valoracion.VisitaID = dto.VisitaID;

        await _repository.UpdateAsync(valoracion);
    }

    public async Task DeleteAsync(int id)
    {
        var valoracion = await _repository.GetByIdAsync(id);
        if (valoracion == null) throw new Exception("Valoración no encontrada.");
        await _repository.DeleteAsync(valoracion);
    }
}