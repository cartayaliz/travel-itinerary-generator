namespace Turist.Services.DTOs.Reporte;

public record ReporteCorrelacionEsfuerzoValoracionDto(
    string NombrePaquete,
    string NivelEsfuerzo,
    double PromedioValoracion,
    int TotalValoraciones
);