using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

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
        // 1. Obtener todas las actividades disponibles (que heredan de Parada)
        var poolActividades = await _actividadRepository.GetAllAsync();

        var query = poolActividades.AsQueryable();

        // 2. Aplicar Criterio: Nivel de Esfuerzo Físico
        if (parameters.NivelEsfuerzoMaximo.HasValue)
        {
            query = query.Where(a => a.NivelEsfuerzo <= parameters.NivelEsfuerzoMaximo.Value);
        }

        // 3. Aplicar Criterio: Tipo de Actividad (si se especificaron filtros)
        if (parameters.TiposActividadPermitidos != null && parameters.TiposActividadPermitidos.Any())
        {
            query = query.Where(a => parameters.TiposActividadPermitidos.Contains(a.Tipo));
        }

        // 4. Aplicar Criterio: Distribución de Categorías (asegurar variedad agrupando por CategoriaID)
        var actividadesFiltradas = query.ToList();
        
        var seleccionadasPorCategoria = actividadesFiltradas
            .GroupBy(a => a.CategoriaID)
            .SelectMany(g => g.Take(Math.Max(1, parameters.CoberturaCategoriasMinima / Math.Max(1, actividadesFiltradas.Select(x => x.CategoriaID).Distinct().Count()))))
            .ToList();

        // Si la selección por categorías se queda corta, rellenamos con el resto filtrado
        var seleccionFinal = seleccionadasPorCategoria
            .Union(actividadesFiltradas)
            .Distinct()
            .OrderBy(a => a.HoraInicio) // Respeta estrictamente el orden cronológico del día
            .Take(parameters.CantidadTotal) // Respeta la cantidad total solicitada
            .ToList();

        // 5. Construir el Itinerario y asociar sus parámetros junto a él
        var itinerario = new Itinerario
        {
            Nombre = $"Itinerario Adaptado - {DateTime.UtcNow:yyyyMMdd}",
            // Aquí enlazaras los parámetros si los tienes mapeados en tu entidad Itinerario
        };

        // 6. Poblar la tabla intermedia (Itinerario_Parada)
        foreach (var actividad in seleccionFinal)
        {
            itinerario.ItinerarioParadas.Add(new Itinerario_Parada
            {
                ParadaID = actividad.ParadaID, // Heredado de Parada
                Itinerario = itinerario
            });
        }

        return itinerario;
    }
}