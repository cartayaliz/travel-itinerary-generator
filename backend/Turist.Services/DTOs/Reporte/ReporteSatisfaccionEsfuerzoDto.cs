namespace Turist.Services.DTOs.Reporte;

public record ReporteSatisfaccionEsfuerzoDto(
    string NombreActividad,
    string NivelEsfuerzo,
    double PromedioValoracion,
    int TotalValoraciones
);