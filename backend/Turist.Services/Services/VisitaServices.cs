using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Visita;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class VisitaService : IVisitaService
{
    private readonly IVisitaRepository _repository;

    public VisitaService(IVisitaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VisitaResponseDto>> GetAllAsync()
    {
        var visitas = await _repository.GetAllAsync();
        return visitas.Select(v => new VisitaResponseDto(
            v.VisitaID, v.Fecha, v.Estado, v.ItinerarioID, v.TuristaID, v.ParadaID
        ));
    }

    public async Task<VisitaResponseDto> GetByIdAsync(int id)
    {
        var visita = await _repository.GetByIdAsync(id);
        if (visita == null) throw new Exception("Visita no encontrada.");

        return new VisitaResponseDto(
            visita.VisitaID, visita.Fecha, visita.Estado,
            visita.ItinerarioID, visita.TuristaID, visita.ParadaID
        );
    }

    public async Task CreateAsync(VisitaCreateDto dto)
    {
        var visita = new Visita
        {
            Fecha = dto.Fecha,
            Estado = dto.Estado,
            ItinerarioID = dto.ItinerarioID,
            TuristaID = dto.TuristaID,
            ParadaID = dto.ParadaID
        };
        await _repository.AddAsync(visita);
    }

    public async Task UpdateAsync(int id, VisitaCreateDto dto)
    {
        var visita = await _repository.GetByIdAsync(id);
        if (visita == null) throw new Exception("Visita no encontrada.");

        visita.Fecha = dto.Fecha;
        visita.Estado = dto.Estado;
        visita.ItinerarioID = dto.ItinerarioID;
        visita.TuristaID = dto.TuristaID;
        visita.ParadaID = dto.ParadaID;

        await _repository.UpdateAsync(visita);
    }

    public async Task DeleteAsync(int id)
    {
        var visita = await _repository.GetByIdAsync(id);
        if (visita == null) throw new Exception("Visita no encontrada.");
        await _repository.DeleteAsync(visita);
    }
}