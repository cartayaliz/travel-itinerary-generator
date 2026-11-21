namespace Turist.Domain.Reportes;

public class ReporteComparacionInterPaquete
{
    public string Paquete { get; set; } = string.Empty;
    public int TotalItinerarios { get; set; }
    public int TotalParadas { get; set; }

    public Dictionary<string, int> DistribucionCategorias { get; set; } = new();
    public Dictionary<string, int> DistribucionEsfuerzo { get; set; } = new();
}