namespace Turist.Domain.Models;

public class Atraccion
{
    public int AtraccionID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int CategoriaID { get; set; }
    public int GuiaID { get; set; }              
    public string Ubicacion { get; set; } = string.Empty;
    public double Latitud { get; set; }
    public double Longitud { get; set; }

    // Navegación
    public Categoria Categoria { get; set; } = null!;
    public GuiaTuristico Guia { get; set; } = null!;
    public List<Actividad_Atraccion> ActividadAtracciones { get; set; } = new();
}