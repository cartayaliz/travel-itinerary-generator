namespace Turist.Domain.Models;

public abstract class Parada
{
    public int ParadaID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public int CategoriaID { get; set; }
    public int GuiaID { get; set; }

    // Navegación
    public Categoria Categoria { get; set; } = null!;
    public GuiaTuristico Guia { get; set; } = null!;
    public List<Itinerario_Parada> ItinerarioParadas { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
}