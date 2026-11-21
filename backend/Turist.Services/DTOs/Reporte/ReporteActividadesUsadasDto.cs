namespace Turist.Services.DTOs.Reporte;

public record ReporteActividadesUsadasDto(
    string NombreActividad,
    string Categoria,
    string NivelEsfuerzo,
    int CantidadUsos
);