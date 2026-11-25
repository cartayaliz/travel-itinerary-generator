using System.Runtime.CompilerServices;

namespace Turist.Domain.Reportes;

public class ReporteSatisfaccion
{
    public string NombreActividad{get; set;} = string.Empty;
    public string Esfuerzo { get; set; } = string.Empty;
    public double Promedio { get; set; }
    public int Total { get; set; }
}