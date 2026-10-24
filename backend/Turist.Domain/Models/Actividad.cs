namespace Turist.Domain.Models;

public class Actividad : Parada
{
    public string Tipo { get; set; } = string.Empty; // Visita, Excursión, Taller
    public string NivelEsfuerzo { get; set; } = string.Empty; // Bajo, Medio, Alto
    public TimeOnly HoraInicio { get; set; }
    public int DuracionMinutos { get; set; }
}