namespace Turist.Services.DTOs.Reporte;

public record ReporteComparacionPaquetesDto(
    string NombrePaquete,
    int TotalItinerarios,
    int TotalParadas,
    double PromedioEsfuerzo 
);