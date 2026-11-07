namespace Turist.Domain.Reportes;

public class ReporteComparacion
{
    public string Paquete { get; set; } = string.Empty;
    public int Itinerarios { get; set; }
    public int Paradas { get; set; }
    public double PromedioEsfuerzo { get; set; }
}