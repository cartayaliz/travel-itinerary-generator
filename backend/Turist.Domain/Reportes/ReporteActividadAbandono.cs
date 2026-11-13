namespace Turist.Domain.Reportes;

public class ReporteActividadAbandono
{
    public string NombreActividad { get; set; } = string.Empty;
    public string NombreGuia { get; set; } = string.Empty;
    public string NombrePaquete { get; set; } = string.Empty;
    public double TasaAbandono { get; set; }
    public int TotalVisitas { get; set; }
    public int VisitasAbandonadas { get; set; }
}