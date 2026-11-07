using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;
using Turist.Services.Interfaces;
namespace Turist.Services.Services;
public class CriteriosItineraryGenerationStrategy : IItineraryGenerationStrategy
    {
        private readonly IParadaRepository _paradaRepository;

        public CriteriosItineraryGenerationStrategy(IParadaRepository paradaRepository)
        {
            _paradaRepository = paradaRepository;
        }

        public async Task<IEnumerable<Parada>> GenerateAsync(ItineraryGenerationParametersDto dto)
        {
            //Obtener todas las paradas (incluye actividades y atracciones)
            var paradasDisponibles = await _paradaRepository.GetAllAsync();

            if (paradasDisponibles == null || !paradasDisponibles.Any())
            {
                throw new InvalidOperationException("No hay paradas registradas en el sistema.");
            }

            //Filtrar utilizando los valores del DTO recibido
            var paradasFiltradas = paradasDisponibles
                .Where(parada => CumpleFiltrosDto(parada, dto))
                .Take(dto.CantidadTotal)
                .ToList();

            if (!paradasFiltradas.Any())
            {
                throw new InvalidOperationException("Ninguna parada cumple con los criterios especificados.");
            }

            return paradasFiltradas;
        }
        private bool CumpleFiltrosDto(Parada parada, ItineraryGenerationParametersDto dto)
        {
            // Validación específica si es una Actividad
            if (parada is Actividad actividad)
            {
                if (dto.NivelEsfuerzoMaximo.HasValue && actividad.NivelEsfuerzo > dto.NivelEsfuerzoMaximo.Value)
                {
                    return false;
                }

                if (dto.TiposActividadPermitidos != null && 
                    dto.TiposActividadPermitidos.Any() && 
                    !dto.TiposActividadPermitidos.Contains(actividad.Tipo)) 
                {
                    return false;
                }
            }

            // Validación específica si es una Atracción (horarios)
            if (parada is Atraccion atraccion)
            {
                if (dto.HoraInicio.HasValue && dto.HorarioCierre.HasValue && atraccion.HorarioApertura > dto.HorarioCierre.Value)
                {
                    return false;
                }

                if (dto.HorarioCierre.HasValue && atraccion.HorarioCierre < dto.HorarioCierre.Value)
                {
                    return false;
                }
            }

            return true;
        }
}