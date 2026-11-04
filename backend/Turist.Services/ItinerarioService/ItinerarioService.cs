using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Interfaces.Services;
using Turist.Domain.Models;
namespace Turist.Services.ItinerarioService;
public class ItinerarioService : IItinerarioService
    {
        private readonly IParadaRepository _paradaRepository;
        private readonly IItineraryGenerationStrategy _generationStrategy;
        private readonly IItinerarioRepository _itinerarioRepository;

        public ItinerarioService(
            IParadaRepository paradaRepository,
            IItineraryGenerationStrategy generationStrategy,
            IItinerarioRepository itinerarioRepository)
        {
            _paradaRepository = paradaRepository;
            _generationStrategy = generationStrategy;
            _itinerarioRepository = itinerarioRepository;
        }

        public async Task<Itinerario> GenerarItinerarioAutomaticoAsync(ItineraryGenerationParameters parameters)
        {
            if (parameters.CantidadTotal <= 0)
                throw new ArgumentException("La cantidad total debe ser mayor a cero.");

            var seleccionFinal = await _generationStrategy.GenerateAsync(parameters);

            if (!seleccionFinal.Any())
                throw new InvalidOperationException("No hay actividades que cumplan con los criterios.");

            var itinerario = new Itinerario
            {
                Nombre = $"Itinerario Automático - {DateTime.UtcNow:yyyyMMdd-HHmmssfff}",
                FechaCreacion = DateTime.UtcNow,
                GuiaID = parameters.GuiaID,
            };

            foreach (var actividad in seleccionFinal)
            {
                itinerario.ItinerarioParadas.Add(new Itinerario_Parada
                {
                    ParadaID = actividad.ParadaID,
                    Itinerario = itinerario
                });
            }

            await _itinerarioRepository.AddAsync(itinerario);

            return itinerario;
        }
    }