using System.ComponentModel;
using System.Runtime.CompilerServices;
using Turist.Domain.Classes;
using Turist.Domain.Interfaces;

namespace Turist.Infrastructure.Services;

/// <summary>
/// Representa el recorrido turístico de varias localizaciones geográficas y con horarios fijos.
/// </summary>
public class Route : IRoute
{
    public DateTime StartTime { get; }
    public List<IVisitableLocation> Steps { get; }
    public Route(List<IVisitableLocation> steps, DateTime startTime)
    {
        Steps = steps;
        StartTime = startTime;
    }
}

/// <summary>
/// Resultado de validación de una ruta. Muestra los conflictos entre horarios y tiempos de espera.
/// </summary>
public class RouteValidationResult(IEnumerable<string> problems)
{
    public List<string> Problems { get; private set; } = [.. problems];
}

public class RouteSystem
{
    private enum Location { ACT, ATT }
    private readonly IWeightedGraph<double> _graph;
    private List<int, int, IVisitableLocation)> _activities = [];
    public RouteSystem(IEnumerable<IVisitableLocation> activities,
                       IEnumerable<IVisitableLocation> attractions,
                       IEnumerable<ITransportation> availableTransportations)
    {
        foreach (var item in activities)
        {
            _activities.Add(GetNode(item.Id, ), );
        }
        _graph = BuildGraph(activities, attractions, availableTransportations);
    }

    /// <summary>
    /// Recibe el identificador de la actividad o atracción, y devuelve el identificador del nodo en el grafo.
    /// </summary>
    /// <param name="locId"></param>
    /// <returns></returns>
    private int GetNode(int locId) => locId;

    /// <summary>
    /// Dado un nodo devuelve el identificador de la localización que representa.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int GetLocationId(int node) => node;

    /// <summary>
    /// Construye el grafo dado las actividades y atracciones, las relacionan entre sí según la red de transporte disponible.
    /// </summary>
    /// <param name="activities"></param>
    /// <param name="attractions"></param>
    /// <param name="transpWeb"></param>
    /// <returns></returns>
    private IWeightedGraph<double> BuildGraph(IEnumerable<IVisitableLocation> activities,
                                              IEnumerable<IVisitableLocation> attractions,
                                              IEnumerable<ITransportation> transpWeb)
    {
        var graph = new WeightedGraph<double>();
        foreach (var activity in activities)
        {
            int node = GetNode(activity.Id);
            graph.AddNode(node);
        }
        foreach (var attraction in attractions)
        {
            int node = GetNode(attraction.Id);
            graph.AddNode(node);
        }
        foreach (var transp in transpWeb)
        {
            double weight = transp.TotalCost + transp.TotalTime; ;

            int n1 = GetNode(transp.StartLocationId);
            if (!graph.HasNode(n1)) continue;

            int n2 = GetNode(transp.StopLocationId);
            if (!graph.HasNode(n1)) continue;

            graph.AddEdge(n1, n2, weight);
        }
        return graph;
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

            if (loc is null)
            {
                problems.Add($"No existe nodo:'{step.Id}'.");
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

    private Route? GetRoute(List<int> path, DateTime startTime)
    {
        List<IVisitableLocation> locations = [];
        foreach (var n in path)
        {
            locations.Add(GetLocation(n));
        }
        Route route = new(locations, startTime);
        return IsValidRoute(route).Problems.Count == 0 ? route : null;
    }

    public Route GetMinRoute(DateTime startTime, int from, int to)
    {
        List<int> path = INodeSet.GetPath(_graph.DijkstraGetMst(from).pi, from, to);
        var steps = new List<IVisitableLocation>();
        foreach (var node in path)
        {
            steps.Add(GetLocation(node));
        }
        return new(steps, startTime);
    }
}