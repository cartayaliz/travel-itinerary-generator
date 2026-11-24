using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Visita;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class VisitaService : IVisitaService
{
    private readonly IVisitaRepository _repository;
    private readonly IItinerarioRepository _itinerarioRepository;

    public VisitaService(IVisitaRepository repository, IItinerarioRepository itinerarioRepository)
    {
        _repository = repository;
        _itinerarioRepository = itinerarioRepository;
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

    //==========
    //Registrar una visita realizada por un turista, cambiado la version anterior a revibir turista id
    //==========

public async Task RegistrarVisitaAsync(RegistrarVisitaDto dto, int turistaId)
{
    var itinerario = await _itinerarioRepository.GetByIdAsync(dto.ItinerarioID);

    if (itinerario == null)
        throw new Exception("Itinerario no encontrado.");

    var turistaAsignado = await _itinerarioRepository
        .IsTuristaAssignedAsync(dto.ItinerarioID, turistaId);

    if (!turistaAsignado)
        throw new Exception("El turista no está asignado a este itinerario.");

    var yaVisitado = await _repository.ExisteVisitaAsync(
        dto.ItinerarioID,
        turistaId,
        dto.ParadaID
    );

    if (yaVisitado)
        throw new Exception("El turista ya ha registrado una visita para esta parada en este itinerario.");

    var visita = new Visita
    {
        Fecha = DateTime.UtcNow,
        Estado = dto.Estado,
        ItinerarioID = dto.ItinerarioID,
        TuristaID = turistaId,
        ParadaID = dto.ParadaID
    };

    await _repository.AddAsync(visita);
}
    public async Task<IEnumerable<VisitaResponseDto>> GetByTuristaIdAsync(int turistaId)
{
    var visitas = await _repository.GetByTuristaIdAsync(turistaId);

    return visitas.Select(v => new VisitaResponseDto(
        v.VisitaID,
        v.Fecha,
        v.Estado,
        v.ItinerarioID,
        v.TuristaID,
        v.ParadaID
    ));
}
}