namespace Turist.Api.Models;

public class PaqueteTuristico
{
    public int PaqueteID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;

    // Navegación
    public List<Autorizado> Autorizados { get; set; } = new();
    public List<Clasifica> Clasificaciones { get; set; } = new();
    public List<Inscribir> Inscripciones { get; set; } = new();
    public List<Itinerario> Itinerarios { get; set; } = new();
}