using Turist.Domain.Enums;
namespace Turist.Domain.Models;

public class Itinerario
{
    public int ItinerarioID { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int GuiaID { get; set; }

    //Campos para guardar estadisticas de la generacion automatica 
    public OrigenItinerario Origen { get; set; }
    public int? CantidadActividades { get; set; }
    public int? ProporcionVisitas { get; set; }
    public int? ProporcionExcursiones { get; set; }
    public int? ProporcionTalleres { get; set; }
    public int? CoberturaCategorias { get; set; }


    // Navegación
    public GuiaTuristico Guia { get; set; } = null!;
    public List<Itinerario_Paquete> ItinerarioPaquetes { get; set; } = new();
    public List<Itinerario_Parada> ItinerarioParadas { get; set; } = new();
    public List<Itinerario_Turista> ItinerarioTuristas { get; set; } = new();
    public List<Validacion> Validaciones { get; set; } = new();
    public List<Modificacion> Modificaciones { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
    public List<Valoracion> Valoraciones { get; set; } = new();
}