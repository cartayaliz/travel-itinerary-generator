using Turist.Services.DTOs.Reporte;

namespace Turist.Services.Interfaces;

public interface IReporteService
{
    Task<IEnumerable<ReporteItinerariosAutomaticosPorPaqueteDto>> GetItinerariosAutomaticosPorPaqueteAsync(int paqueteId);
    Task<IEnumerable<ReporteActividadesUsadasDto>> GetActividadesMasUsadasAsync();
    Task<IEnumerable<ReporteSatisfaccionEsfuerzoDto>> GetSatisfaccionPorEsfuerzoAsync();
    Task<IEnumerable<ReporteComparacionPaquetesDto>> GetComparacionPaquetesAsync();
}