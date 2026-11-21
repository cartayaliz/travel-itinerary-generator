namespace Turist.Services.DTOs.Reporte;

public record ReporteComparacionPaquetesDto(
    string NombrePaquete,
    int TotalItinerarios,
    int TotalParadas,
    Dictionary<string, int> DistribucionCategorias,
    Dictionary<string, int> DistribucionEsfuerzo
);