using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
public class CriteriosItineraryGenerationStrategy : IItineraryGenerationStrategy
    {
        private readonly IParadaRepository _paradaRepository;

        public CriteriosItineraryGenerationStrategy(IParadaRepository paradaRepository)
        {
            _paradaRepository = paradaRepository;
        }

        public async Task<IEnumerable<Actividad>> GenerateAsync(ItineraryGenerationParameters parameters)
        {
            var filterOptions = new ActividadFilterOptions
            {
                EsfuerzoMaximo = parameters.NivelEsfuerzoMaximo,
                TiposPermitidos = parameters.TiposActividadPermitidos ?? new()
            };

            var actividadesFiltradas = await _paradaRepository.GetParaGeneracionAsync(filterOptions);

            // Algoritmo de selección: agrupa por categoría, asegura cobertura y ordena cronológicamente
            return actividadesFiltradas
                .GroupBy(a => a.CategoriaID)
                .SelectMany(g => g.Take(Math.Max(1, parameters.CoberturaCategoriasMinima / Math.Max(1, actividadesFiltradas.Select(x => x.CategoriaID).Distinct().Count()))))
                .Union(actividadesFiltradas)
                .Distinct()
                .OrderBy(a => a.HoraInicio)
                .Take(parameters.CantidadTotal)
                .ToList();
        }
}