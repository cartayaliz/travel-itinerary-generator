using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Services.DTOs.Itinerario;
using Turist.Services.Interfaces;
using Turist.Domain.Graph;
namespace Turist.Services.Services;
public class CriteriosItineraryGenerationStrategy : IItineraryGenerationStrategy
    {
        private readonly IParadaRepository _paradaRepository;
        private readonly ITransportWebService _transportWebService;

        public CriteriosItineraryGenerationStrategy(IParadaRepository paradaRepository, ITransportWebService transportWebService)
        {
            _paradaRepository = paradaRepository;
            _transportWebService = transportWebService;
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
                .ToList();

            if (!paradasFiltradas.Any())
            {
                throw new InvalidOperationException("Ninguna parada cumple con los criterios especificados.");
            }

            await _transportWebService.LoadAsync();

            return OrdenarPorGrafo(paradasFiltradas, dto.CantidadTotal);
        }
        private List<Parada> OrdenarPorGrafo(List<Parada> candidatas, int cantidadTotal)
        {
            var resultado = new List<Parada>();
            var restantes = candidatas
                .GroupBy(p => p.ParadaID)
                .Select(g => g.First())
                .ToList();

            if (!restantes.Any())
                return resultado;

            // Elegimos un nodo inicial presente en el grafo o el primero disponible
            var actual = restantes.FirstOrDefault(p => _transportWebService.GetGraphNodeIds().Contains(p.ParadaID))
                        ?? restantes.First();

            resultado.Add(actual);
            restantes.Remove(actual);

            while (resultado.Count < cantidadTotal && restantes.Any())
            {
                Parada? siguienteConectada = null;
                Weight? menorPeso = null;

                //Buscar la candidata restante alcanzable en el grafo con menor costo acumulado
                foreach (var candidata in restantes)
                {
                    if (_transportWebService.TryGetShortestWeight(actual.ParadaID, candidata.ParadaID, out Weight peso))
                    {
                        if (menorPeso is null || peso.CompareTo(menorPeso.Value) < 0)
                        {
                            menorPeso = peso;
                            siguienteConectada = candidata;
                        }
                    }
                }

                if (siguienteConectada != null)
                {
                    resultado.Add(siguienteConectada);
                    restantes.Remove(siguienteConectada);
                    actual = siguienteConectada;
                    continue;
                }

                // Si ninguna restante tiene camino en el grafo desde actual
                // elegimos la más cercana en distancia directa para no perder paradas válidas
                var masCercana = restantes
                    .OrderBy(p => DistanceCalculator.ShorterDistance(
                        actual.Latitud, actual.Longitud,
                        p.Latitud, p.Longitud))
                    .First();

                resultado.Add(masCercana);
                restantes.Remove(masCercana);
                actual = masCercana;
            }

            return resultado;
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