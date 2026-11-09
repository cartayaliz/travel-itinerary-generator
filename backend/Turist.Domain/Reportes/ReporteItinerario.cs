namespace Turist.Domain.Reportes;

public class ReporteItinerario
{
    public int ItinerarioID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public int GuiaID { get; set; }
    public int? CantidadActividades { get; set; }
    public int? ProporcionVisitas { get; set; }
    public int? ProporcionExcursiones { get; set; }
    public int? ProporcionTalleres { get; set; }
    public int? CoberturaCategorias { get; set; }
}