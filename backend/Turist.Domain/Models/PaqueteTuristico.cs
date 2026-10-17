namespace Turist.Domain.Models;

public class PaqueteTuristico
{
    public int PaqueteID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;

    // Navegación
    public List<Guia_Paquete> GuiaPaquetes { get; set; } = new();
    public List<Paquete_Categoria> PaqueteCategorias { get; set; } = new();
    public List<Turista_Paquete> TuristaPaquetes { get; set; } = new();
    public List<Itinerario> Itinerarios { get; set; } = new();
}