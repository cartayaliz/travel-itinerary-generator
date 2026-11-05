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
}