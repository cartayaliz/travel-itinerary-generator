namespace Turist.Domain.Reportes;

public class ReporteItinerarioRevisor
{
    public int ItinerarioID { get; set; }
    public string NombreItinerario { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaValidacion { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public string EstadoValidacion { get; set; } = string.Empty;
}