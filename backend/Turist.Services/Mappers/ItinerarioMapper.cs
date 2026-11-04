using Turist.Services.DTOs.Itinerario;
using Turist.Domain.Models;
using Turist.Domain.Enums;
namespace Turist.Services.Mappers
{
    public static class ItinerarioMapper
    {
        public static ItinerarioResponseDto ToResponseDto(Itinerario itinerario)
        {
            if (itinerario == null) return null!;

            // Filtramos y extraemos las actividades desde la tabla intermedia de forma segura
            var actividades = itinerario.ItinerarioParadas?
                .Select(ip => ip.Parada)
                .OfType<Actividad>()
                .ToList() ?? new List<Actividad>();

            return new ItinerarioResponseDto(
                ItinerarioID: itinerario.ItinerarioID,
                Nombre: itinerario.Nombre,
                Origen: itinerario.Origen,
                FechaCreacion: itinerario.FechaCreacion,
                GuiaID: itinerario.GuiaID,
                CantidadActividades: actividades.Count,
                
                // Cálculo de proporciones basado en los tipos de actividad
                ProporcionVisitas: actividades.Count(a => a.Tipo == TipoActividad.Excursion),
                ProporcionExcursiones: actividades.Count(a => a.Tipo == TipoActividad.Taller),
                ProporcionTalleres: actividades.Count(a => a.Tipo == TipoActividad.Visita),
                
                // Conteo de categorías distintas cubiertas
                CoberturaCategorias: actividades.Select(a => a.CategoriaID).Distinct().Count()
            );
        }
    }
}