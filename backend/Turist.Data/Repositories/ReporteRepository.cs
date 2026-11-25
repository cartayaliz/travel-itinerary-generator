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
            // Hacemos un Join con GuiasTuristicos para obtener el Nombre
            .Join(_context.GuiasTuristicos,
                  itinerario => itinerario.GuiaID,
                  guia => guia.UsuarioID,
                  (itinerario, guia) => new { itinerario, guia })
            .Select(x => new ReporteItinerario
            {
                ItinerarioID = x.itinerario.ItinerarioID,
                Nombre = x.itinerario.Nombre,
                FechaCreacion = x.itinerario.FechaCreacion,
                NombreCreador = x.guia.Nombre,
                CantidadActividades = x.itinerario.CantidadActividades,
                ProporcionVisitas = x.itinerario.ProporcionVisitas,
                ProporcionExcursiones = x.itinerario.ProporcionExcursiones,
                ProporcionTalleres = x.itinerario.ProporcionTalleres,
                CoberturaCategorias = x.itinerario.CoberturaCategorias
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
        .Where(v => v.Visita != null && v.Visita.Parada is Actividad)
        .Select(v => new
        {
            Actividad = (Actividad)v.Visita!.Parada!,
            v.Puntuacion
        })
        // Agrupamos por Id, Nombre y NivelEsfuerzo para obtener métricas por cada actividad
        .GroupBy(x => new
        {
            x.Actividad.ParadaID, 
            x.Actividad.Nombre,
            x.Actividad.NivelEsfuerzo
        })
        .Select(g => new ReporteSatisfaccion
        {
            NombreActividad = g.Key.Nombre,
            Esfuerzo = g.Key.NivelEsfuerzo.ToString(),
            Promedio = g.Average(x => (double)x.Puntuacion),
            Total = g.Count()
        })
        .ToListAsync();
    }

    /// <summary>
    /// REPORTE 5: Compara los paquetes turísticos según la cantidad de itinerarios, paradas
    /// y el promedio de esfuerzo físico de sus actividades.
    /// Se traen las entidades a memoria para hacer el cálculo de agregación con LINQ en C#.
    /// </summary>
    public async Task<IEnumerable<ReporteComparacionInterPaquete>> GetComparacionPaquetesAsync()
    {
        var paquetes = await _context.PaquetesTuristicos
            .Include(p => p.ItinerarioPaquetes)
                .ThenInclude(ip => ip.Itinerario)
                    .ThenInclude(i => i.ItinerarioParadas)
                        .ThenInclude(ipar => ipar.Parada)
                            .ThenInclude(par => par.Categoria) // <-- Incluimos la categoría
            .ToListAsync();

        return paquetes.Select(p =>
        {
            var paradas = p.ItinerarioPaquetes
                .SelectMany(ip => ip.Itinerario.ItinerarioParadas)
                .ToList();

            var actividades = paradas
                .Where(ip => ip.Parada is Actividad)
                .Select(ip => (Actividad)ip.Parada)
                .ToList();

            // Distribución por Categoría
            var distCategorias = paradas
                .GroupBy(ip => ip.Parada.Categoria.Nombre)
                .ToDictionary(g => g.Key, g => g.Count());

            // Distribución por Nivel de Esfuerzo
            var distEsfuerzo = actividades
                .GroupBy(a => a.NivelEsfuerzo.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            return new ReporteComparacionInterPaquete
            {
                Paquete = p.Nombre,
                TotalItinerarios = p.ItinerarioPaquetes.Count,
                TotalParadas = paradas.Count,
                DistribucionCategorias = distCategorias,
                DistribucionEsfuerzo = distEsfuerzo
            };
        });
    }
    /// <summary>
    /// REPORTE 6: Determina para cada paquete turístico, la correlación entre
    ///  el nivel de esfuerzo físico de las actividades y la valoración promedio de los turistas.
    /// </summary>
    public async Task<IEnumerable<ReporteCorrelacionEsfuerzoValoracion>> GetCorrelacionEsfuerzoValoracionAsync()
    {
        // Partimos de los paquetes turísticos para relacionarlos con sus itinerarios
        return await _context.Itinerario_Paquetes
            .SelectMany(ip => ip.Itinerario.ItinerarioParadas, (ip, parada) => new { ip.Paquete, parada.Parada })
            .Join(
                _context.Set<Actividad>(), // Aseguramos que la parada es una Actividad para obtener el NivelEsfuerzo
                x => x.Parada.ParadaID,
                a => a.ParadaID,
                (x, actividad) => new { x.Paquete, actividad.NivelEsfuerzo, x.Parada.ItinerarioParadas }
            )
            // Vinculamos con las valoraciones de los itinerarios de esas paradas
            .SelectMany(x => x.ItinerarioParadas.Select(ip => ip.Itinerario), (x, itinerario) => new { x.Paquete, x.NivelEsfuerzo, itinerario.Valoraciones })
            .SelectMany(x => x.Valoraciones, (x, valoracion) => new
            {
                NombrePaquete = x.Paquete.Nombre,
                NivelEsfuerzo = x.NivelEsfuerzo.ToString(),
                valoracion.Puntuacion
            })
            .GroupBy(x => new { x.NombrePaquete, x.NivelEsfuerzo })
            .Select(g => new ReporteCorrelacionEsfuerzoValoracion
            {
                NombrePaquete = g.Key.NombrePaquete,
                NivelEsfuerzo = g.Key.NivelEsfuerzo,
                PromedioValoracion = g.Average(v => (double)v.Puntuacion),
                TotalValoraciones = g.Count()
            })
            .ToListAsync();
    }

    /// <summary>
    /// REPORTE 6: Identifica las 10 actividades con la tasa de abandono más alta,
    /// el guía que las creó y el paquete turístico al que pertenecen.
    /// </summary>
    public async Task<IEnumerable<ReporteActividadAbandono>> GetTop10ActividadesAbandonoAsync()
    {
        return await _context.Visitas
            .GroupBy(v => v.ParadaID)
            .Select(g => new
            {
                ParadaID = g.Key,
                TotalVisitas = g.Count(),
                VisitasAbandonadas = g.Count(v => v.Estado == EstadoVisita.Abandonada),
                TasaAbandono = g.Count() > 0 ? (double)g.Count(v => v.Estado == EstadoVisita.Abandonada) / g.Count() * 100 : 0
            })
            .Join(_context.Set<Actividad>(), x => x.ParadaID, a => a.ParadaID, (x, actividad) => new { x, actividad })
            .Join(_context.GuiasTuristicos, x => x.actividad.GuiaID, g => g.UsuarioID, (x, guia) => new { x.x, x.actividad, guia })
            .Join(_context.Itinerario_Paradas, x => x.actividad.ParadaID, ip => ip.ParadaID, (x, ip) => new { x.x, x.actividad, x.guia, ip.ItinerarioID })
            .Join(_context.Itinerario_Paquetes, x => x.ItinerarioID, ip => ip.ItinerarioID, (x, ip) => new { x.x, x.actividad, x.guia, ip.PaqueteID })
            .Join(_context.PaquetesTuristicos, x => x.PaqueteID, p => p.PaqueteID, (x, paquete) => new ReporteActividadAbandono
            {
                NombreActividad = x.actividad.Nombre,
                NombreGuia = x.guia.Nombre,
                NombrePaquete = paquete.Nombre,
                TasaAbandono = x.x.TasaAbandono,
                TotalVisitas = x.x.TotalVisitas,
                VisitasAbandonadas = x.x.VisitasAbandonadas
            })
            .Distinct()
            .OrderByDescending(r => r.TasaAbandono)
            .Take(10)
            .ToListAsync();
    }
    /// REPORTE 6: Compara el desempeño de los turistas que solicitaron una modificación del itinerario 
    /// con el promedio general de valoraciones de sus respectivos grupos de viaje para esos mismos paquetes turísticos.
    /// </summary>
    public async Task<IEnumerable<ReporteComparacionModificacion>> GetComparacionModificacionesGrupoAsync()
    {
        var paquetes = await _context.PaquetesTuristicos
            .Include(p => p.ItinerarioPaquetes)
                .ThenInclude(ip => ip.Itinerario)
                    .ThenInclude(i => i.Modificaciones)
            .Include(p => p.ItinerarioPaquetes)
                .ThenInclude(ip => ip.Itinerario)
                    .ThenInclude(i => i.Valoraciones)
                        .ThenInclude(v => v.Turista)
                            .ThenInclude(t => t.Grupo)
            .ToListAsync();

        var reportes = new List<ReporteComparacionModificacion>();

        // Procesar las métricas por paquete e itinerario
        foreach (var paquete in paquetes)
        {
            foreach (var ip in paquete.ItinerarioPaquetes)
            {
                var itinerario = ip.Itinerario;

                // Turistas que tienen solicitudes de modificación registradas en este itinerario
                var turistasConModificacionIds = itinerario.Modificaciones
                    .Select(m => m.TuristaID)
                    .Distinct()
                    .ToList();

                if (!turistasConModificacionIds.Any())
                    continue;

                // Obtener todas las valoraciones hechas sobre este itinerario
                var valoracionesItinerario = itinerario.Valoraciones
                    .Where(v => v.ItinerarioID == itinerario.ItinerarioID)
                    .ToList();

                if (!valoracionesItinerario.Any())
                    continue;

                foreach (var turistaId in turistasConModificacionIds)
                {
                    var valoracionTuristaModificado = valoracionesItinerario
                        .FirstOrDefault(v => v.TuristaID == turistaId);

                    if (valoracionTuristaModificado == null)
                        continue;

                    var turista = valoracionTuristaModificado.Turista;

                    // Calcular el promedio general de valoraciones del resto de miembros de su grupo de viaje
                    var valoracionesRestoGrupo = valoracionesItinerario
                        .Where(v => v.Turista.GrupoID == turista.GrupoID && v.TuristaID != turistaId)
                        .Select(v => (double)v.Puntuacion)
                        .ToList();

                    double promedioGrupo = valoracionesRestoGrupo.Any()
                        ? valoracionesRestoGrupo.Average()
                        : (double)valoracionTuristaModificado.Puntuacion; //si el grupo solo tiene un integrante

                    reportes.Add(new ReporteComparacionModificacion
                    {
                        TuristaID = turista.UsuarioID,
                        TuristaNombre = turista.Nombre,
                        PaqueteNombre = paquete.Nombre,
                        PromedioTuristaModificado = valoracionTuristaModificado.Puntuacion,
                        PromedioGrupo = Math.Round(promedioGrupo, 2)
                    });
                }
            }
        }
        return reportes;
    }

}