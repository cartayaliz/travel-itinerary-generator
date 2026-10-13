using System.Reflection.Metadata;
using Turist.Domain.Interfaces;

namespace Turist.Infrastructure.Services.RouteSystem;

/// <summary>
/// Representa el recorrido turístico de varias localizaciones geográficas y con horarios fijos.
/// </summary>
public class Route : IRoute
{
    public DateTime StartTime {get;}
    public List<IVisitableLocation> Steps {get;}
    public Route(List<IVisitableLocation> steps, DateTime startTime)
    {
        Steps = steps;
        StartTime = startTime;
    }
}

/// <summary>
/// Chequeo de validación de una ruta. Muestra los conflictos entre horarios y tiempos de espera.
/// </summary>
public class RouteValidationResult
{
    public List<string> Problems {get; private set;}
    public RouteValidationResult(IEnumerable<string> problems) => Problems = [.. problems];
    public void AddProblem(string problem) => Problems.Add(problem);
}

public class RouteSystem
{
    private readonly IWeightedGraph _graph;
    private GraphFactory _factory;
    private IEnumerable<IVisitableLocation> _locations;
    public RouteSystem(IEnumerable<IVisitableLocation> activities,
                       IEnumerable<IVisitableLocation> attractions,
                       IEnumerable<ITransportation> availableTransportations)
    {
        _factory = new();
        _graph = _factory.Build(activities, attractions, availableTransportations);
        _locations = activities.Concat(attractions);
    }

    /// <summary>
    /// Devuelve el objeto IVisitableLocation a partir de un indentificador. 
    /// </summary>
    /// <param name="locId"></param>
    /// <returns></returns>
    private IVisitableLocation? GetLocation(int locId)
    {
        return _locations.FirstOrDefault(l => l.Id == locId);
    }

    /// <summary>
    /// Revisa si una ruta dada es válida según los tiempos asignados a cada visita y horarios de cada localización. 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    public RouteValidationResult IsValidRoute(IRoute route)
    {
        var problems = new List<string>();
        var now = route.StartTime;

        foreach (var step in route.Steps)
        {
            IVisitableLocation? loc = GetLocation(step.Id);
            
            if (loc is null) 
            {
                problems.Add($"No existe localización con ID:'{step.Id}'.");
                continue;
            }
            if (!loc.Schedule.IsOpen(now))
            { 
                problems.Add($"'{loc.Name}' está cerrado al llegar ({now:HH:mm}). Abre a las {loc.Schedule.NextAperture(now):HH:mm}");
                continue;
            }

            var desiredExitTime = now + loc.VisitDelay;
            if (!loc.Schedule.IsOpen(desiredExitTime))
            {
                problems.Add($"No se alcanza a visitar '{loc.Name}'."
                );
                continue;
            }

            now = desiredExitTime;
        }

        return new RouteValidationResult(problems);
    }

    public IRoute GetRoute(int from, int to)
    {
        throw new NotImplementedException();
    }

}