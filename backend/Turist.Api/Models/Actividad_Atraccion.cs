namespace Turist.Api.Models;

public class Actividad_Atraccion
{
    public int ActividadID { get; set; }
    public int AtraccionID { get; set; }

    // Navegación
    public Actividad Actividad { get; set; } = null!;
    public Atraccion Atraccion { get; set; } = null!;
}