namespace Turist.Api.Models;

public class GuiaTuristico
{
    public int GuiaID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especialidad { get; set; } = string.Empty;
    public bool EsJefe { get; set; }

    // Navegación
    public List<Autorizado> Autorizados { get; set; } = new();
    public List<Itinerario> Itinerarios { get; set; } = new();
    public List<Actividad> Actividades { get; set; } = new();
    public List<Validacion> Validaciones { get; set; } = new();
    public List<Modificacion> Modificaciones { get; set; } = new();
}