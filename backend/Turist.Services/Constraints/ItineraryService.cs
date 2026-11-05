using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;
using Turist.Services.Interfaces;

namespace Turist.Services.Constraints;

public class ItineraryService : IItineraryService
{
    private readonly IItinerarioRepository _repository;

    public ItineraryService(IItinerarioRepository repository)
    {
        _repository = repository;
    }

    public async Task AsignarTuristasAsync(int itinerarioId, List<AsignarTuristaDto> turistasDto)
    {
        var itinerario = await _repository.GetByIdAsync(itinerarioId);
        if (itinerario == null)
        {
            throw new KeyNotFoundException($"No se encontró el itinerario con ID {itinerarioId}.");
        }

        var turistasDuplicados = turistasDto
            .GroupBy(t => t.TuristaID)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (turistasDuplicados.Any())
        {
            throw new InvalidOperationException($"El itinerario no puede tener al mismo turista asignado 2 veces. TuristaID(s) duplicado(s): {string.Join(", ", turistasDuplicados)}");
        }

        await _repository.LimpiarTuristasAsync(itinerarioId);

        var turistas = turistasDto.Select(dto => new Itinerario_Turista
        {
            ItinerarioID = itinerarioId,
            TuristaID = dto.TuristaID,
            FechaAsignacion = dto.FechaAsignacion
        }).ToList();

        await _repository.AsignarTuristasAsync(turistas);
    }

    public async Task AsignarParadasAsync(int itinerarioId, List<AsignarParadaDto> paradasDto)
    {
        var itinerario = await _repository.GetByIdAsync(itinerarioId);
        if (itinerario == null)
        {
            throw new KeyNotFoundException($"No se encontró el itinerario con ID {itinerarioId}.");
        }

        if (paradasDto.GroupBy(p => p.Orden).Any(g => g.Count() > 1))
        {
            throw new InvalidOperationException("No se puede asignar el mismo número de orden a dos paradas.");
        }

        var paradasDuplicadas = paradasDto
            .GroupBy(p => p.ParadaID)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (paradasDuplicadas.Any())
        {
            throw new InvalidOperationException($"El itinerario no puede tener la misma parada asignada 2 veces. ParadaID(s) duplicado(s): {string.Join(", ", paradasDuplicadas)}");
        }

        await _repository.LimpiarParadasAsync(itinerarioId);

        var paradas = paradasDto.Select(dto => new Itinerario_Parada
        {
            ItinerarioID = itinerarioId,
            ParadaID = dto.ParadaID,
            Orden = dto.Orden
        }).ToList();

        await _repository.AsignarParadasAsync(paradas);
    }
}