using Turist.Services.DTOs.Reporte;

namespace Turist.Services.Interfaces;

public interface IReporteService
{
    Task<IEnumerable<ReporteActividadesUsadasDto>> GetActividadesMasUsadasAsync();
    Task<IEnumerable<ReporteSatisfaccionEsfuerzoDto>> GetSatisfaccionPorEsfuerzoAsync();
    Task<IEnumerable<ReporteComparacionPaquetesDto>> GetComparacionPaquetesAsync();
}