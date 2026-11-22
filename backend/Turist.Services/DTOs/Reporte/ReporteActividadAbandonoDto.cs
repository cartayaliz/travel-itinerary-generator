namespace Turist.Services.DTOs.Reporte;

public record ReporteActividadAbandonoDto(
    string NombreActividad,
    string NombreGuia,
    string NombrePaquete,
    double TasaAbandono,
    int TotalVisitas,
    int VisitasAbandonadas
);