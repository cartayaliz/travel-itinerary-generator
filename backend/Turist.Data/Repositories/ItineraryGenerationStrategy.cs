using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Domain.Enums;

namespace Turist.Data.Repositories;

public class CriteriosItineraryGenerationStrategy : IItineraryGenerationStrategy
{
    private readonly IActividadRepository _actividadRepository;

    public CriteriosItineraryGenerationStrategy(IActividadRepository actividadRepository)
    {
        _actividadRepository = actividadRepository;
    }

    public async Task<Itinerario> GenerateAsync(ItineraryGenerationParameters parameters)
    {
        //Obtener actividades filtradas
        var actividadesFiltradas = await _actividadRepository.GetParaGeneracionAsync(
            parameters.NivelEsfuerzoMaximo, 
            parameters.TiposActividadPermitidos
        );

        //Selección final según criterios y cantidad total
        var seleccionFinal = actividadesFiltradas
            .GroupBy(a => a.CategoriaID)
            .SelectMany(g => g.Take(Math.Max(1, parameters.CoberturaCategoriasMinima / Math.Max(1, actividadesFiltradas.Select(x => x.CategoriaID).Distinct().Count()))))
            .Union(actividadesFiltradas)
            .Distinct()
            .OrderBy(a => a.HoraInicio) // Orden cronológico
            .Take(parameters.CantidadTotal)
            .ToList();

        //Crear el Itinerario
        var itinerario = new Itinerario
        {
            Nombre = $"Itinerario Adaptado - {DateTime.UtcNow:yyyyMMdd-HHmmssfff}",
            FechaCreacion = DateTime.UtcNow,
            Origen = OrigenItinerario.Automatico,
            GuiaID = parameters.GuiaID,
            CantidadActividades = seleccionFinal.Count,
            CoberturaCategorias = seleccionFinal.Select(a => a.CategoriaID).Distinct().Count(),
 
        };

        //Enlazar las actividades a la tabla intermedia
        foreach (var actividad in seleccionFinal)
        {
            itinerario.ItinerarioParadas.Add(new Itinerario_Parada
            {
                ParadaID = actividad.ParadaID,
                Itinerario = itinerario
            });
        }

        return itinerario;
    }
}