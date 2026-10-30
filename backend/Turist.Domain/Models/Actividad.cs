using Turist.Domain.Enums;
namespace Turist.Domain.Models;

public class Actividad : Parada
{
    public TipoActividad Tipo { get; set; }
    public NivelEsfuerzo NivelEsfuerzo { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public int DuracionMinutos { get; set; }
}
public class ItineraryGenerationParameters
{
    public int CantidadTotal { get; set; }
    
    // Nivel de esfuerzo físico deseado
    public NivelEsfuerzo? NivelEsfuerzoMaximo { get; set; }

    // Tipos de actividad permitidos o preferidos
    public List<TipoActividad> TiposActividadPermitidos { get; set; } = new();

    // Cobertura mínima de categorías distintas
    public int CoberturaCategoriasMinima { get; set; }
}