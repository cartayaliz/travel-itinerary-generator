using Microsoft.EntityFrameworkCore;
using Turist.Domain.Enums;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Domain.Reportes;

namespace Turist.Data.Repositories;

public class ReporteRepository : IReporteRepository
{
    private readonly TuristDbContext _context;

    public ReporteRepository(TuristDbContext context) => _context = context;

    /// <summary>
    /// REPORTE 1: Obtiene el listado de itinerarios generados automáticamente para un paquete turístico específico, 
    /// indicando el nombre del creador, la fecha de creación y los parámetros utilizados.
    /// </summary>
    public async Task<IEnumerable<ReporteItinerario>> GetAutomaticosByPaqueteIdAsync(int paqueteId)
    {
        return await _context.Itinerarios
            .Where(i => i.Origen == OrigenItinerario.Automatico && 
                        i.ItinerarioPaquetes.Any(ip => ip.PaqueteID == paqueteId))
            .Select(i => new ReporteItinerario
            {
                ItinerarioID = i.ItinerarioID,
                Nombre = i.Nombre,
                FechaCreacion = i.FechaCreacion,
                GuiaID = i.GuiaID,
                CantidadActividades = i.CantidadActividades,
                ProporcionVisitas = i.ProporcionVisitas,
                ProporcionExcursiones = i.ProporcionExcursiones,
                ProporcionTalleres = i.ProporcionTalleres,
                CoberturaCategorias = i.CoberturaCategorias
            })
            .ToListAsync();
    }
    /// <summary>
    /// REPORTE 2: Obtiene las actividades más utilizadas en los itinerarios.
    /// Agrupa las paradas que son de tipo 'Actividad' por su Nombre, Categoría y Nivel de Esfuerzo.
    /// Devuelve el conteo de cuántas veces aparece cada actividad en los itinerarios.
    /// </summary>
    public async Task<IEnumerable<ReporteActividad>> GetActividadesMasUsadasAsync()
    {
        return await _context.Itinerario_Paradas
            // Hacemos JOIN con la Parada y su Categoría para obtener los datos descriptivos
            .Include(ip => ip.Parada)
                .ThenInclude(p => p.Categoria)
            // Filtramos solo las paradas que son Actividades (ignoramos Atracciones)
            .Where(ip => ip.Parada is Actividad)
            // Agrupamos por los criterios del reporte
            .GroupBy(ip => new
            {
                Nombre = ip.Parada.Nombre,
                Categoria = ip.Parada.Categoria.Nombre,
                Esfuerzo = ((Actividad)ip.Parada).NivelEsfuerzo
            })
            // Proyectamos los resultados al modelo de dominio
            .Select(g => new ReporteActividad
            {
                Nombre = g.Key.Nombre,
                Categoria = g.Key.Categoria,
                Esfuerzo = g.Key.Esfuerzo.ToString(),
                Cantidad = g.Count()
            })
            .OrderByDescending(r => r.Cantidad) // Ordenamos de mayor a menor uso
            .ToListAsync();
    }

    /// <summary>
    /// REPORTE 3: Lista los itinerarios que fueron validados por un revisor determinado,
    /// indicando la fecha de validación y las observaciones hechas durante el proceso.
    /// </summary>
    public async Task<IEnumerable<ReporteItinerarioRevisor>> GetItinerariosValidadosByJefeIdAsync(int jefeId)
    {
        return await _context.Validaciones
            .Where(v => v.JefeID == jefeId)
            .Select(v => new ReporteItinerarioRevisor
            {
                ItinerarioID = v.Itinerario.ItinerarioID,
                NombreItinerario = v.Itinerario.Nombre,
                FechaCreacion = v.Itinerario.FechaCreacion,
                FechaValidacion = v.FechaValidacion,
                Observaciones = v.Observaciones,
                EstadoValidacion = v.Estado.ToString()
            })
            .ToListAsync();
    }

    /// <summary>
    /// REPORTE 4: Obtiene la satisfacción (promedio de valoraciones) agrupada por nivel de esfuerzo físico.
    /// Hace un JOIN entre Valoraciones -> Visita -> Parada para saber el nivel de esfuerzo de la actividad valorada.
    /// </summary>
    public async Task<IEnumerable<ReporteSatisfaccion>> GetSatisfaccionPorEsfuerzoAsync()
    {
        return await _context.Valoraciones
            // Navegamos desde Valoración hasta la Parada a través de la Visita
            .Include(v => v.Visita)
                .ThenInclude(vis => vis!.Parada)
            // Solo consideramos valoraciones asociadas a una Visita cuya Parada sea una Actividad
            .Where(v => v.Visita != null && v.Visita.Parada is Actividad)
            // Agrupamos por el nivel de esfuerzo
            .GroupBy(v => ((Actividad)v.Visita!.Parada).NivelEsfuerzo)
            // Calculamos el promedio de puntuación y el total de valoraciones por grupo
            .Select(g => new ReporteSatisfaccion
            {
                Esfuerzo = g.Key.ToString(),
                Promedio = g.Average(v => (double)v.Puntuacion),
                Total = g.Count()
            })
            .ToListAsync();
    }

    /// <summary>
    /// REPORTE 5: Compara los paquetes turísticos según la cantidad de itinerarios, paradas
    /// y el promedio de esfuerzo físico de sus actividades.
    /// Se traen las entidades a memoria para hacer el cálculo de agregación con LINQ en C#.
    /// </summary>
    public async Task<IEnumerable<ReporteComparacion>> GetComparacionPaquetesAsync()
    {
        // Traemos los paquetes con todas sus relaciones anidadas en una sola consulta
        var paquetes = await _context.PaquetesTuristicos
            .Include(p => p.ItinerarioPaquetes) // Paquete -> Itinerarios
                .ThenInclude(ip => ip.Itinerario)
                    .ThenInclude(i => i.ItinerarioParadas) // Itinerario -> Paradas
                        .ThenInclude(ipar => ipar.Parada) // Parada (para saber el esfuerzo)
            .ToListAsync();

        // Realizamos los cálculos de agregación en memoria (C#) para mayor claridad
        return paquetes.Select(p =>
        {
            // Aplanamos todas las paradas de todos los itinerarios del paquete
            var paradas = p.ItinerarioPaquetes
                .SelectMany(ip => ip.Itinerario.ItinerarioParadas)
                .ToList();

            // Filtramos solo las Actividades para calcular el esfuerzo promedio
            var actividades = paradas
                .Where(ip => ip.Parada is Actividad)
                .Select(ip => (int)((Actividad)ip.Parada).NivelEsfuerzo)
                .ToList();

            // Construimos el modelo de reporte
            return new ReporteComparacion
            {
                Paquete = p.Nombre,
                Itinerarios = p.ItinerarioPaquetes.Count,
                Paradas = paradas.Count,
                PromedioEsfuerzo = actividades.Any() ? actividades.Average() : 0
            };
        });
    }
}