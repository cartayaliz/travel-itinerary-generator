using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;
using Turist.Services.Interfaces;
using Turist.Domain.Enums;

namespace Turist.Services.Services;

public class ItinerarioService : IItinerarioService
{
    private readonly IItinerarioRepository _repository;
    private readonly IGuiaRepository _guiaRepository;
    private readonly IItineraryGenerationStrategy _generationStrategy;

    public ItinerarioService(
         IItinerarioRepository repository,
         IGuiaRepository guiaRepository, IItineraryGenerationStrategy generationStrategy)
    {
        _repository = repository;
        _guiaRepository = guiaRepository;
        _generationStrategy = generationStrategy;
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
        var itinerario = await _repository.GetByIdAsync(itinerarioId);
        if (itinerario == null)
        {
            throw new KeyNotFoundException($"No se encontró el itinerario con ID {itinerarioId}.");
        }

        if (paradas.GroupBy(p => p.Orden).Any(g => g.Count() > 1))
        {
            throw new InvalidOperationException("No se puede asignar el mismo número de orden a dos paradas.");
        }

        var paradasDuplicadas = paradas
            .GroupBy(p => p.ParadaID)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (paradasDuplicadas.Any())
        {
            throw new InvalidOperationException($"El itinerario no puede tener la misma parada asignada 2 veces. ParadaID(s) duplicado(s): {string.Join(", ", paradasDuplicadas)}");
        }

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
        //Buscamos el itinerario para saber qué guía lo creó
        var itinerario = await _repository.GetByIdAsync(itinerarioId);
        if (itinerario == null) throw new Exception("Itinerario no encontrado.");

        //  Validamos que el guía esté autorizado
        foreach (var p in paquetes)
        {
            var isAuthorized = await _guiaRepository.IsAuthorizedForPackageAsync(itinerario.GuiaID, p.PaqueteID);
            if (!isAuthorized)
            {
                throw new Exception($"El guía con ID {itinerario.GuiaID} no está autorizado para el paquete {p.PaqueteID}.");
            }
        }

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
        var itinerario = await _repository.GetByIdAsync(itinerarioId);
        if (itinerario == null)
        {
            throw new KeyNotFoundException($"No se encontró el itinerario con ID {itinerarioId}.");
        }

        var turistasDuplicados = turistas
            .GroupBy(t => t.TuristaID)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (turistasDuplicados.Any())
        {
            throw new InvalidOperationException($"El itinerario no puede tener al mismo turista asignado 2 veces. TuristaID(s) duplicado(s): {string.Join(", ", turistasDuplicados)}");
        }

        await _repository.LimpiarTuristasAsync(itinerarioId);

        var lista = turistas.Select(t => new Itinerario_Turista
        {
            ItinerarioID = itinerarioId,
            TuristaID = t.TuristaID,
            FechaAsignacion = t.FechaAsignacion
        }).ToList();

        await _repository.AsignarTuristasAsync(lista);
    }
    public async Task<Itinerario> GenerarItinerarioAutomaticoAsync(ItineraryGenerationParametersDto dto)
    {
        if (dto.CantidadTotal <= 0)
            throw new ArgumentException("La cantidad total debe ser mayor a cero.");

        var seleccionFinal = (await _generationStrategy.GenerateAsync(dto)).ToList();

        if (!seleccionFinal.Any())
            throw new InvalidOperationException("No hay actividades que cumplan con los criterios.");

        //Extraer las actividades de la selección.
        var actividades = seleccionFinal.OfType<Actividad>().ToList();

        var itinerario = new Itinerario
        {
            Nombre = $"Itinerario Automático - {DateTime.UtcNow:yyyyMMdd-HHmmssfff}",
            FechaCreacion = DateTime.UtcNow,
            GuiaID = dto.GuiaID,
            Origen = OrigenItinerario.Automatico,

            //Asignar los campos estadísticos directamente en la entidad.
            CantidadActividades = actividades.Count,
            ProporcionVisitas = actividades.Count(a => a.Tipo == TipoActividad.Visita),
            ProporcionExcursiones = actividades.Count(a => a.Tipo == TipoActividad.Excursion),
            ProporcionTalleres = actividades.Count(a => a.Tipo == TipoActividad.Taller),
            CoberturaCategorias = actividades.Select(a => a.CategoriaID).Distinct().Count()
        };

        // Asignar las paradas con su orden correspondiente.
        for (int i = 0; i < seleccionFinal.Count; i++)
        {
            itinerario.ItinerarioParadas.Add(new Itinerario_Parada
            {
                ParadaID = seleccionFinal[i].ParadaID,
                Orden = i + 1, //Guardar el orden de la ruta.
                Itinerario = itinerario
            });
        }

        await _repository.AddAsync(itinerario);

        return itinerario;
    }   
}