using Turist.Services.DTOs.Reporte;

namespace Turist.Services.Interfaces;

public interface IReporteService
{
    Task<IEnumerable<ReporteItinerariosAutomaticosPorPaqueteDto>> GetItinerariosAutomaticosPorPaqueteAsync(int paqueteId);
    Task<IEnumerable<ReporteActividadesUsadasDto>> GetActividadesMasUsadasAsync();
    Task<IEnumerable<ReporteItinerariosPorRevisorDto>> GetItinerariosValidadosPorJefeAsync(int jefeId);
    Task<IEnumerable<ReporteSatisfaccionEsfuerzoDto>> GetSatisfaccionPorEsfuerzoAsync();
    Task<IEnumerable<ReporteComparacionPaquetesDto>> GetComparacionPaquetesAsync();
    Task<IEnumerable<ReporteCorrelacionEsfuerzoValoracionDto>> GetCorrelacionEsfuerzoValoracionAsync();
    Task<IEnumerable<ReporteActividadAbandonoDto>> GetTop10ActividadesAbandonoAsync();
    Task<IEnumerable<ReporteComparacionModificacionDto>> GetComparacionModificacionesGrupoAsync();
}