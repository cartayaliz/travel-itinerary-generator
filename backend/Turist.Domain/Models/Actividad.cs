using Turist.Domain.Enums;
namespace Turist.Domain.Models;

public class Actividad : Parada
{
    public TipoActividad Tipo { get; set; }
    public NivelEsfuerzo NivelEsfuerzo { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public int DuracionMinutos { get; set; }
}