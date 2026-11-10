using Turist.Domain.Reportes;

namespace Turist.Domain.Interfaces.Repositories;

public interface IReporteRepository
{
    Task<IEnumerable<ReporteActividad>> GetActividadesMasUsadasAsync();
    Task<IEnumerable<ReporteSatisfaccion>> GetSatisfaccionPorEsfuerzoAsync();
    Task<IEnumerable<ReporteComparacion>> GetComparacionPaquetesAsync();
    Task<IEnumerable<ReporteItinerario>> GetAutomaticosByPaqueteIdAsync(int paqueteId);
    Task<IEnumerable<ReporteItinerarioRevisor>> GetItinerariosValidadosByJefeIdAsync(int jefeId);
}