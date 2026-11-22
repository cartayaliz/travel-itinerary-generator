using Microsoft.Extensions.Caching.Distributed;
using Turist.Domain.Interfaces.Repositories;
using Turist.Services.DTOs.Reporte;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ReporteService : IReporteService
{
    private readonly IReporteRepository _repository;
    private readonly IDistributedCache _cache;
    
    public ReporteService(IReporteRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public Task<IEnumerable<ReporteItinerariosAutomaticosPorPaqueteDto>> GetItinerariosAutomaticosPorPaqueteAsync(int paqueteId)
        => _cache.GetOrCreateAsync<IEnumerable<ReporteItinerariosAutomaticosPorPaqueteDto>>(
                $"reportes:auto:{paqueteId}",
                async () => [.. (await _repository.GetAutomaticosByPaqueteIdAsync(paqueteId))
                    .Select(m => new ReporteItinerariosAutomaticosPorPaqueteDto(m.ItinerarioID,
                        m.Nombre,
                        m.FechaCreacion,
                        m.NombreCreador,
                        m.CantidadActividades,
                        m.ProporcionVisitas,
                        m.ProporcionExcursiones,
                        m.ProporcionTalleres,
                        m.CoberturaCategorias))],
                TimeSpan.FromMinutes(10));

    public Task<IEnumerable<ReporteActividadesUsadasDto>> GetActividadesMasUsadasAsync()
        => _cache.GetOrCreateAsync<IEnumerable<ReporteActividadesUsadasDto>>(
                "reportes:actividades-mas-usadas",
                async () => [.. (await _repository.GetActividadesMasUsadasAsync())
                    .Select(m => new ReporteActividadesUsadasDto(m.Nombre, m.Categoria, m.Esfuerzo, m.Cantidad))],
                TimeSpan.FromMinutes(10));

    public Task<IEnumerable<ReporteItinerariosPorRevisorDto>> GetItinerariosValidadosPorJefeAsync(int jefeId)
        => _cache.GetOrCreateAsync<IEnumerable<ReporteItinerariosPorRevisorDto>>(
            $"reportes:validados:{jefeId}",
            async () => [.. (await _repository.GetItinerariosValidadosByJefeIdAsync(jefeId))
                .Select(m => new ReporteItinerariosPorRevisorDto(
                    m.ItinerarioID,m.NombreItinerario,
                    m.FechaCreacion,
                    m.FechaValidacion,
                    m.Observaciones,
                    m.EstadoValidacion))],
            TimeSpan.FromMinutes(10));

    public Task<IEnumerable<ReporteSatisfaccionEsfuerzoDto>> GetSatisfaccionPorEsfuerzoAsync()
        => _cache.GetOrCreateAsync<IEnumerable<ReporteSatisfaccionEsfuerzoDto>>(
                "reportes:satisfaccion-esfuerzo",
                async () => [.. (await _repository.GetSatisfaccionPorEsfuerzoAsync())
                    .Select(m => new ReporteSatisfaccionEsfuerzoDto(m.Esfuerzo, m.Promedio, m.Total))],
                TimeSpan.FromMinutes(10));

    public Task<IEnumerable<ReporteComparacionPaquetesDto>> GetComparacionPaquetesAsync()
        => _cache.GetOrCreateAsync<IEnumerable<ReporteComparacionPaquetesDto>>(
                "reportes:comparacion-paquetes",
                async () => [.. (await _repository.GetComparacionPaquetesAsync())
                    .Select(m => new ReporteComparacionPaquetesDto(
                        m.Paquete,
                        m.TotalItinerarios,
                        m.TotalParadas,
                        m.DistribucionCategorias,
                        m.DistribucionEsfuerzo))],
            TimeSpan.FromMinutes(10));

    public Task<IEnumerable<ReporteCorrelacionEsfuerzoValoracionDto>> GetCorrelacionEsfuerzoValoracionAsync()
        => _cache.GetOrCreateAsync<IEnumerable<ReporteCorrelacionEsfuerzoValoracionDto>>(
                "reportes:correlacion-esfuerzo-valoracion",
                async () => [.. (await _repository.GetCorrelacionEsfuerzoValoracionAsync())
                    .Select(m => new ReporteCorrelacionEsfuerzoValoracionDto(
                        m.NombrePaquete,
                        m.NivelEsfuerzo,
                        Math.Round(m.PromedioValoracion, 2),
                        m.TotalValoraciones))],
            TimeSpan.FromMinutes(10));

    public Task<IEnumerable<ReporteActividadAbandonoDto>> GetTop10ActividadesAbandonoAsync()
        => _cache.GetOrCreateAsync<IEnumerable<ReporteActividadAbandonoDto>>(
                "reportes:top-10-actividades-abandono",
                async () => [.. (await _repository.GetTop10ActividadesAbandonoAsync())
                    .Select(m => new ReporteActividadAbandonoDto(
                            m.NombreActividad,
                            m.NombreGuia,
                            m.NombrePaquete,
                            Math.Round(m.TasaAbandono, 2),
                            m.TotalVisitas,
                            m.VisitasAbandonadas))],
                TimeSpan.FromMinutes(10));

    public Task<IEnumerable<ReporteComparacionModificacionDto>> GetComparacionModificacionesGrupoAsync()
        => _cache.GetOrCreateAsync<IEnumerable<ReporteComparacionModificacionDto>>(
                "reportes:comparacion-modificaciones-grupo",
                async () => [.. (await _repository.GetComparacionModificacionesGrupoAsync())
                    .Select(m => new ReporteComparacionModificacionDto(
                        m.TuristaID,
                        m.TuristaNombre,
                        m.PaqueteNombre,
                        m.PromedioTuristaModificado,
                        m.PromedioGrupo))],
                TimeSpan.FromMinutes(10));
}