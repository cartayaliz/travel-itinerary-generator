namespace Turist.Domain.Reportes;
public class ReporteComparacionModificacion
{
    public int TuristaID { get; set; }
    public string TuristaNombre { get; set; } = string.Empty;
    public string PaqueteNombre { get; set; } = string.Empty;
    public double PromedioTuristaModificado { get; set; }
    public double PromedioGrupo { get; set; }
}