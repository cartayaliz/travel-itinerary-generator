using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Validacion;
using Turist.Services.DTOs.Valoracion;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ValoracionService : IValoracionService
{
    private readonly IValoracionRepository _repository;
    private readonly IVisitaRepository _visitaRepository;

    public ValoracionService(IValoracionRepository repository, IVisitaRepository visitaRepository)
    {
        _repository = repository;
        _visitaRepository = visitaRepository;
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
    //=========
    //Para que un turista haga una valoracion exclusivamente a una visita o a un itinerario
    //=========
    public async Task ValorarAsync(ValorarDto dto)
    {
        // 1. Validar Arco Exclusivo y Puntuación (Lo que ya teníamos)
        if (dto.ItinerarioID.HasValue == dto.VisitaID.HasValue)
            throw new Exception("La valoración debe estar asociada a un Itinerario o a una Visita.");
        if (dto.Puntuacion < 1 || dto.Puntuacion > 5)
            throw new Exception("La puntuación debe estar entre 1 y 5.");

        // 2. Verificar si ya existe una valoración de este turista
        Valoracion? valoracionExistente = null;

        if (dto.VisitaID.HasValue)
        {
            valoracionExistente = await _repository.GetByVisitaAndTuristaAsync(dto.TuristaID, dto.VisitaID.Value);
        }
        else if (dto.ItinerarioID.HasValue)
        {
            valoracionExistente = await _repository.GetByItinerarioAndTuristaAsync(dto.TuristaID, dto.ItinerarioID.Value);
        }

        // 3. Si existe, la actualizamos (Edición)
        if (valoracionExistente != null)
        {
            valoracionExistente.Puntuacion = dto.Puntuacion;
            valoracionExistente.Comentario = dto.Comentario;
            valoracionExistente.Fecha = DateTime.UtcNow; // Actualizamos la fecha de edición
            await _repository.UpdateAsync(valoracionExistente);
            return;
        }

        // 4. Si no existe, la creamos
        var nuevaValoracion = new Valoracion
        {
            Comentario = dto.Comentario,
            Fecha = DateTime.UtcNow,
            Puntuacion = dto.Puntuacion,
            TuristaID = dto.TuristaID,
            ItinerarioID = dto.ItinerarioID,
            VisitaID = dto.VisitaID
        };

        await _repository.AddAsync(nuevaValoracion);
    }
}