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
            var paradas = itinerario.ItinerarioParadas?.Select(ip => ip.Parada).ToList() ?? new List<Parada>();
            var actividades = paradas.OfType<Actividad>().ToList();

            return new ItinerarioResponseDto(
                ItinerarioID: itinerario.ItinerarioID,
                Nombre: itinerario.Nombre,
                Origen: itinerario.Origen,
                FechaCreacion: itinerario.FechaCreacion,
                GuiaID: itinerario.GuiaID,
                CantidadActividades: itinerario.CantidadActividades ?? actividades.Count,
                ProporcionVisitas: itinerario.ProporcionVisitas ?? actividades.Count(a => a.Tipo == TipoActividad.Visita),
                ProporcionExcursiones: itinerario.ProporcionExcursiones ?? actividades.Count(a => a.Tipo == TipoActividad.Excursion),
                ProporcionTalleres: itinerario.ProporcionTalleres ?? actividades.Count(a => a.Tipo == TipoActividad.Taller),
                CoberturaCategorias: itinerario.CoberturaCategorias ?? actividades.Select(a => a.CategoriaID).Distinct().Count()
            );
        }
    }
}