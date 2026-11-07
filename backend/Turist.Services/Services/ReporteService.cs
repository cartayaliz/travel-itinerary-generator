using Turist.Domain.Interfaces.Repositories;
using Turist.Services.DTOs.Reporte;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ReporteService : IReporteService
{
    private readonly IReporteRepository _repository;

    public ReporteService(IReporteRepository repository) => _repository = repository;

    public async Task<IEnumerable<ReporteActividadesUsadasDto>> GetActividadesMasUsadasAsync()
    {
        var modelos = await _repository.GetActividadesMasUsadasAsync();
        return modelos.Select(m => new ReporteActividadesUsadasDto(m.Nombre, m.Categoria, m.Esfuerzo, m.Cantidad));
    }

    public async Task<IEnumerable<ReporteSatisfaccionEsfuerzoDto>> GetSatisfaccionPorEsfuerzoAsync()
    {
        var modelos = await _repository.GetSatisfaccionPorEsfuerzoAsync();
        return modelos.Select(m => new ReporteSatisfaccionEsfuerzoDto(m.Esfuerzo, m.Promedio, m.Total));
    }

    public async Task<IEnumerable<ReporteComparacionPaquetesDto>> GetComparacionPaquetesAsync()
    {
        var modelos = await _repository.GetComparacionPaquetesAsync();
        return modelos.Select(m => new ReporteComparacionPaquetesDto(m.Paquete, m.Itinerarios, m.Paradas, m.PromedioEsfuerzo));
    }
}