using Turist.Domain.Enums;
namespace Turist.Domain.Models;

public class Actividad : Parada
{
    public TipoActividad Tipo { get; set; }
    public NivelEsfuerzo NivelEsfuerzo { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public int DuracionMinutos { get; set; }
}
public class ActividadFilterOptions
{
    public NivelEsfuerzo? EsfuerzoMaximo { get; set; }
    public List<TipoActividad> TiposPermitidos { get; set; } = new();
    public TimeOnly? HorarioAperturaMinimo { get; set; }
    public TimeOnly? HorarioCierreMaximo { get; set; }
}