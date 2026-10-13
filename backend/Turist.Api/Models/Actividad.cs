using Turist.Domain.Interfaces;

namespace Turist.Api.Models;

public class Actividad
{
    public int ActividadId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string NivelEsfuerzo { get; set; } = string.Empty;
    public int GuiaId { get; set; }
    public int CategoriaId { get; set; }

    // Navegación
    public GuiaTuristico Guia { get; set; } = null!;
    public Categoria Categoria { get; set; } = null!;
    public List<Contiene> Contiene { get; set; } = new();
    public List<Visita> Visitas { get; set; } = new();
    
    //Grafo
    public int LocationId
    {
        get => ActividadId;
        set => ActividadId = value;
    }
}