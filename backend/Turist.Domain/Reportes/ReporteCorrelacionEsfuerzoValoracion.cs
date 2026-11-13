namespace Turist.Domain.Reportes;

public class ReporteCorrelacionEsfuerzoValoracion
{
    public string NombrePaquete { get; set; } = string.Empty;
    public string NivelEsfuerzo { get; set; } = string.Empty;
    public double PromedioValoracion { get; set; }
    public int TotalValoraciones { get; set; }
}