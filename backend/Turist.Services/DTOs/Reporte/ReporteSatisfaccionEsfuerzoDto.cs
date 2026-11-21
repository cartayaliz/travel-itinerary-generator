namespace Turist.Services.DTOs.Reporte;

public record ReporteSatisfaccionEsfuerzoDto(
    string NivelEsfuerzo,
    double PromedioValoracion,
    int TotalValoraciones
);