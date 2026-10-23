namespace Turist.Domain.Models;

public class GuiaTuristico : Usuario
{
    public string Especialidad { get; set; } = string.Empty;

    // Navegación 
    public List<Guia_Paquete> GuiaPaquetes { get; set; } = new();
    public List<Itinerario> Itinerarios { get; set; } = new();
    public List<Parada> Paradas { get; set; } = new(); 
    public List<Modificacion> Modificaciones { get; set; } = new();
}