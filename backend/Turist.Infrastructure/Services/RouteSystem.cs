using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Turist.Domain.Classes;
using Turist.Domain.Interfaces;

namespace Turist.Infrastructure.Services;

/// <summary>
/// Representa el recorrido turístico de varias localizaciones geográficas y con horarios fijos.
/// </summary>
public class Route : IRoute
{
    public DateTime StartTime { get; }
    public List<IVisitable> visitablesId { get; }
    public Route(List<IVisitable> steps, DateTime startTime)
    {
        visitablesId = steps;
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
    private readonly IWeightedGraph<double> _graph;
    private readonly Dictionary<int, IVisitable> _visitables = [];
    private readonly Dictionary<(int, int), ITransportation> _transp = [];

    public RouteSystem(IEnumerable<IVisitable> visitables,
                       IEnumerable<ITransportation> transpWeb)
    {
        _graph = BuildGraph(visitables, transpWeb);
    }

    private IWeightedGraph<double> BuildGraph(IEnumerable<IVisitable> visitables,
                                              IEnumerable<ITransportation> transpWeb)
    {
        var graph = new WeightedGraph<double>();
        foreach (var v in visitables)
        {
            int node = _graph.NodesCount;
            graph.AddNode(node);
            _visitables[node] = v;
        }
        foreach (var transp in transpWeb)
        {
            double weight = transp.TravelTimeSpan.Minutes + transp.TotalCost;
            var n1 = transp.StartVisitableId;
            var n2 = transp.StopVisitableId;
            graph.AddEdge(n1, n2, weight);
            _transp[(n1, n2)] = transp;
        }
        return graph;
    }

    public void Update(IEnumerable<IVisitable> visitables,
                       IEnumerable<ITransportation> transpWeb)
    {
        foreach (var v in visitables)
        {
            int node = _graph.NodesCount;
            _graph.AddNode(node);
            _visitables[node] = v;
        }
        foreach (var transp in transpWeb)
        {
            double weight = transp.TravelTimeSpan.Minutes + transp.TotalCost;
            var n1 = transp.StartVisitableId;
            var n2 = transp.StopVisitableId;
            _graph.AddEdge(n1, n2, weight);
            _transp[(n1, n2)] = transp;
        }
    }

    /// <summary>
    /// Revisa si una ruta dada es válida según los tiempos asignados a cada visita y horarios de cada localización. 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    public static RouteValidationResult ValidateRoute(IRoute route)
    {
        var problems = new List<string>();
        var now = route.StartTime;

        foreach (var step in route.visitablesId)
        {
            var loc = step;
            if (loc is null)
            {
                problems.Add($"No existe nodo:'{step.Id}'.");
                continue;
            }
            if (!loc.Schedule.IsOpen(now))
            {
                problems.Add($"'{loc.Id}' está cerrado al llegar ({now:HH:mm}).");
                continue;
            }

            var desiredExitTime = now + loc.Schedule.VisitTimeSpan;
            if (!loc.Schedule.IsOpen(now, desiredExitTime))
            {
                problems.Add($"No se alcanza a visitar '{loc.Id}'."
                );
                continue;
            }

            now = desiredExitTime;
        }

        return new RouteValidationResult(problems);
    }

    private Route? GetRoute(IEnumerable<int> path, DateTime startTime)
    {
        List<IVisitable> visList = [];
        foreach (int node in path)
        {
            visList.Add(_visitables[node]);
        }
        Route route = new(visList, startTime);
        return ValidateRoute(route).Problems.Count == 0 ? route : null;
    }

    public Route? GetBestRoute(DateTime startDt,
                               DateTime stopDt,
                               int start,
                               int stop)
    {
        Dictionary<int, bool> blocked = [];
        foreach (var item in _visitables)
        {
            blocked[item.Key] = item.Value.IsBlocked;
        }
        var path = GetBestVisitedPath(_graph, startDt, stopDt, blocked, start, stop);
        return GetRoute(path, startDt, stopDt);
    }

    private int GetBestVisitedPath(IWeightedGraph<double> graph,
                                  DateTime startDt,
                                  DateTime stopDt,
                                  Dictionary<int, bool> blocked,
                                  int s,
                                  out List<int> bestPath)
    {
        var (count, path) = GbvpBacktrack(s, startDt);
        bestPath = [.. path];
        return count;

        (int count, IEnumerable<int> path) GbvpBacktrack(int currNode,
                                                         DateTime now)
        {
            if (blocked[currNode]) return (0, []);

            var visitTs = _visitables[currNode].Schedule.VisitTimeSpan;
            var departureTime = now + visitTs;
            if (departureTime >= stopDt || !_visitables[currNode].Schedule.IsOpen(now, departureTime))
                return (0, []);

            blocked[currNode] = true;
            int bestCount = 1;
            List<int> bestSubPath = [currNode];

            foreach (var (to, weight) in graph.GetNeighbors(currNode))
            {
                var travelTs = _transp[(currNode, to)].TravelTimeSpan;
                var (childCount, childPath) = GbvpBacktrack(to, departureTime + travelTs);
                if (childCount++ > bestCount)
                {
                    bestCount = childCount;
                    bestSubPath = [currNode, ..childPath];
                }
            }

            blocked[currNode] = false;

            return (bestCount, bestSubPath);
        }
    }
}