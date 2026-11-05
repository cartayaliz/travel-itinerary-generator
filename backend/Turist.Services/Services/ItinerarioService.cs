using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ItinerarioService : IItinerarioService
{
    private readonly IItinerarioRepository _repository;
    private readonly IValidationService _validationService;

    public ItinerarioService(
        IItinerarioRepository repository,
        IValidationService validationService)
    {
        _repository = repository;
        _validationService = validationService;
    }

    public async Task<IEnumerable<ItinerarioResponseDto>> GetAllAsync()
    {
        var itinerarios = await _repository.GetAllAsync();
        return itinerarios.Select(i => new ItinerarioResponseDto(
            i.ItinerarioID, i.Nombre, i.Origen, i.FechaCreacion, i.GuiaID,
            i.CantidadActividades, i.ProporcionVisitas, i.ProporcionExcursiones,
            i.ProporcionTalleres, i.CoberturaCategorias
        ));
    }

    public async Task<ItinerarioResponseDto> GetByIdAsync(int id)
    {
        var itinerario = await _repository.GetByIdAsync(id);
        if (itinerario == null) throw new Exception("Itinerario no encontrado.");

        return new ItinerarioResponseDto(
            itinerario.ItinerarioID, itinerario.Nombre, itinerario.Origen,
            itinerario.FechaCreacion, itinerario.GuiaID,
            itinerario.CantidadActividades, itinerario.ProporcionVisitas,
            itinerario.ProporcionExcursiones, itinerario.ProporcionTalleres,
            itinerario.CoberturaCategorias
        );
    }

    public async Task CreateAsync(ItinerarioCreateDto dto)
    {
        var itinerario = new Itinerario
        {
            Nombre = dto.Nombre,
            Origen = dto.Origen,
            GuiaID = dto.GuiaID,
            FechaCreacion = DateTime.UtcNow,
            CantidadActividades = dto.CantidadActividades,
            ProporcionVisitas = dto.ProporcionVisitas,
            ProporcionExcursiones = dto.ProporcionExcursiones,
            ProporcionTalleres = dto.ProporcionTalleres,
            CoberturaCategorias = dto.CoberturaCategorias
        };

        await _repository.AddAsync(itinerario);
    }

    public async Task UpdateAsync(int id, ItinerarioCreateDto dto)
    {
        var itinerario = await _repository.GetByIdAsync(id);
        if (itinerario == null) throw new Exception("Itinerario no encontrado.");

        itinerario.Nombre = dto.Nombre;
        itinerario.Origen = dto.Origen;
        itinerario.GuiaID = dto.GuiaID;
        itinerario.CantidadActividades = dto.CantidadActividades;
        itinerario.ProporcionVisitas = dto.ProporcionVisitas;
        itinerario.ProporcionExcursiones = dto.ProporcionExcursiones;
        itinerario.ProporcionTalleres = dto.ProporcionTalleres;
        itinerario.CoberturaCategorias = dto.CoberturaCategorias;

        await _repository.UpdateAsync(itinerario);
    }

    public async Task DeleteAsync(int id)
    {
        var itinerario = await _repository.GetByIdAsync(id);
        if (itinerario == null) throw new Exception("Itinerario no encontrado.");

        await _repository.DeleteAsync(itinerario);
    }

    // ==========================================
    // ASIGNACIONES N:M
    // ==========================================

    public async Task AsignarParadasAsync(int itinerarioId, List<AsignarParadaAItinerarioDto> paradas)
    {
        await _repository.LimpiarParadasAsync(itinerarioId);

        var lista = paradas.Select(p => new Itinerario_Parada
        {
            ItinerarioID = itinerarioId,
            ParadaID = p.ParadaID,
            Orden = p.Orden
        }).ToList();

        await _repository.AsignarParadasAsync(lista);
    }

    public async Task AsignarPaquetesAsync(int itinerarioId, List<AsignarPaqueteAItinerarioDto> paquetes)
    {
        // 1. Buscamos el itinerario para saber qué guía lo creó
        var itinerario = await _repository.GetByIdAsync(itinerarioId);
        if (itinerario == null) throw new Exception("Itinerario no encontrado.");

        // 2. Validamos que el guía esté autorizado para TODOS los paquetes que se le asignan
        foreach (var p in paquetes)
        {
            var isAuthorized = await _validationService.IsGuiaAuthorizedAsync(itinerario.GuiaID, p.PaqueteID);
            if (!isAuthorized)
            {
                throw new Exception($"El guía con ID {itinerario.GuiaID} no está autorizado para el paquete {p.PaqueteID}.");
            }
        }

        // 3. Si todo está bien, guardamos
        await _repository.LimpiarPaquetesAsync(itinerarioId);

        var lista = paquetes.Select(p => new Itinerario_Paquete
        {
            ItinerarioID = itinerarioId,
            PaqueteID = p.PaqueteID
        }).ToList();

        await _repository.AsignarPaquetesAsync(lista);
    }

    public async Task AsignarTuristasAsync(int itinerarioId, List<AsignarTuristaAItinerarioDto> turistas)
    {
        await _repository.LimpiarTuristasAsync(itinerarioId);

        var lista = turistas.Select(t => new Itinerario_Turista
        {
            ItinerarioID = itinerarioId,
            TuristaID = t.TuristaID,
            FechaAsignacion = t.FechaAsignacion
        }).ToList();

        await _repository.AsignarTuristasAsync(lista);
    }
}