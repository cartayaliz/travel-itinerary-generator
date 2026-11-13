namespace Turist.Services.DTOs.Reporte;

public record ReporteComparacionModificacionDto(
    int TuristaID, 
    string TuristaNombre, 
    string PaqueteNombre, 
    double PromedioTuristaModificado, 
    double PromedioGrupo
);