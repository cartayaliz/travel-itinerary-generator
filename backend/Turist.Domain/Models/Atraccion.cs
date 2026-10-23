namespace Turist.Domain.Models;

public class Atraccion : Parada
{
    public TimeOnly? HorarioApertura { get; set; }
    public TimeOnly? HorarioCierre { get; set; }
}