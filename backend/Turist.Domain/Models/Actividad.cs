namespace Turist.Domain.Models;

public class Actividad
{
    public int ActividadID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string NivelEsfuerzo { get; set; } = string.Empty;
    public int GuiaID { get; set; }
    public int CategoriaID { get; set; }

    // Navegación
    public GuiaTuristico Guia { get; set; } = null!;
    public Categoria Categoria { get; set; } = null!;
    public List<Itinerario_Actividad> ItinerarioActividades { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
    public List<Actividad_Atraccion> ActividadAtracciones { get; set; } = new();
}