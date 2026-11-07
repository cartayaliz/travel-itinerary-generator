using Turist.Domain.Enums;
namespace Turist.Services.DTOs.Itinerario;
public record ItineraryGenerationParametersDto
(
    int CantidadTotal,
    NivelEsfuerzo? NivelEsfuerzoMaximo,
    List<TipoActividad> TiposActividadPermitidos,    
    int CoberturaCategoriasMinima,
    int GuiaID, 
    TimeOnly? HoraInicio, 
    TimeOnly? HorarioCierre
);