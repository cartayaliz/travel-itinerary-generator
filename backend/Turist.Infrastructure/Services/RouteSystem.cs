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
    public string Print()
    {
        string s = "[ ";
        for (int i = 0; i < visitablesId.Count; i++)
        {
            IVisitable? item = visitablesId[i];
            s = $"{s}{item.Id} ";
            if (i == visitablesId.Count)
            {
                s = $"{s}]";
            }
            else { s = $"{s}|"; }
        }
        return s;
    }
}

/// <summary>
/// Resultado de validación de una ruta. Muestra los conflictos entre horarios y tiempos de espera.
/// </summary>
public class RouteValidationResult(IEnumerable<string> problems)
{
    public List<string> Problems { get; private set; } = [.. problems];
}
/// <summary>
/// Funciones esenciales:
///     - CalcTimeSpan(origenId, destinoId)
///     - CalcCost(origenId, destinoId)
///     - ObtenerLocalesCercanos(atraccionId, radioMinutos)
///     - ProponerAlternativas(localesCancelados)
///     - ValidarRuta(ruta)
///     - ActualizarPesos
/// </summary>
public class RouteSystem
{
    private readonly IWeightedGraph<double> _graph;
    private readonly Dictionary<int, IVisitable> _visitables = [];
    private readonly Dictionary<(int, int), ITransportation> _transpWeb = [];

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
            var n1 = transp.StartVisitableId;
            var n2 = transp.StopVisitableId;
            graph.AddEdge(n1, n2, transp.TravelTimeSpan.TotalMinutes + transp.Cost);
            _transpWeb[(n1, n2)] = transp;
        }
        return graph;
    }

    public void UpdateGraph(IEnumerable<IVisitable> visitables,
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
            var n1 = transp.StartVisitableId;
            var n2 = transp.StopVisitableId;
            _graph.AddEdge(n1, n2, transp.TravelTimeSpan.TotalMinutes + transp.Cost);
            _transpWeb[(n1, n2)] = transp;
        }
    }

    /// <summary>
    /// Valida la factibilidad según los horarios de una ruta dada.
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

    /// <summary>
    /// Propone la mejor valorada ruta desde un origen, en un intervalo de tiempo y con costo máximo.
    /// </summary>
    /// <param name="startDt"></param>
    /// <param name="stopDt"></param>
    /// <param name="origin"></param>
    /// <param name="maxCost"></param>
    /// <returns></returns>
    public Route? GetBestRatedRoute(DateTime startDt,
                                    DateTime stopDt,
                                    int origin,
                                    int maxCost,
                                    Dictionary<int, bool> categories) 
                                    => GetRoute(GetBestRatedPath(startDt, stopDt, maxCost, origin, categories).bestPath, startDt);

    private Route? GetRoute(IEnumerable<int> path, DateTime startTime)
    {
        List<IVisitable> visList = [];
        foreach (int node in path)
        {
            visList.Add(_visitables[node]);
        }
        return new(visList, startTime);
    }

/*
Backtrack(origen, fechaEntrada, 0)
Backtrack(nodoActual, momento, costoAcumulado):
    si costoAcumulado > costoMáximo: retorna (nil, 0)

    horaSalida <- momento
    si nodoActual cancelado o visitado o categoría no aceptable: 
        horaSalida <- momento
    si no:
        visitTs <- tiempo de visita del nodo actual
        horaSalida <- momento + visitTs

    si horaSalida >= fechaSalida o nodoActual no está abierto abierto durante momento y horaSalida: retorna (nil, 0)

    añade nodoActual a visitados
    mejorValor <- nil
    mejorCamino <- [nodoActual]

    Para cada vecino de nodoActual:
        tiempoViaje <- tiempo del viaje del transporte del nodoActual hasta su vecino
        costoViaje <- costo del viaje del transporte del nodoActual hasta su vecino
        horaEntrada <- horaSalida + tiemporViaje
        (subCamino, subValor) = Backtrack(vecino, horaEntrada, costoAcumulado + costoViaje)
        
        si mejorValor = nil: mejorValor <- subValor
        si no:
            si subValor > mejorValor:
                mejorValor <- subValor
                mejorCamino <- [nodoActual] + subCamino
    
    remover nodoActual de visitados

    si nodoActual no está cancelado y con categoría aceptable: mejorValor += valoración(nodoActual)

    retorna mejorCamino y mejorValor
*/

    private (double bestRate, IEnumerable<int> bestPath) GetBestRatedPath(DateTime startDt,
                                                                          DateTime stopDt,
                                                                          double maxCost,
                                                                          int startNode,
                                                                          Dictionary<int, bool> categories)
    {
        Dictionary<int, bool> visited = [];
        return GbrpBacktrack(startNode, startDt, 0);

        (double rate, IEnumerable<int> path) GbrpBacktrack(int currNode,
                                                         DateTime now,
                                                         double accumCost)
        {
            if (accumCost > maxCost) return (0, []);

            var departureHour = now;
            if (!_visitables[currNode].IsBlocked
                && !visited[currNode]
                && !categories[_visitables[currNode].CategoryId])
                departureHour += _visitables[currNode].Schedule.VisitTimeSpan;
            
            if (departureHour >= stopDt
                || !_visitables[currNode].Schedule.IsOpen(now, departureHour))
                return (0, []);

            visited[currNode] = true;
            double bestRate = double.MinValue;
            List<int> bestPath = [currNode];

            foreach (var (to, weight) in _graph.GetNeighbors(currNode))
            {
                var travelTime = _transpWeb[(currNode, to)].TravelTimeSpan;
                var travelCost = _transpWeb[(currNode, to)].Cost;
                var (childCount, childPath) = GbrpBacktrack(to, departureHour + travelTime, travelCost);
                if (childCount > bestRate)
                {
                    bestRate = childCount;
                    bestPath = [currNode, .. childPath];
                }
            }

            visited[currNode] = false;

            if (!_visitables[currNode].IsBlocked
                && categories[_visitables[currNode].CategoryId]) 
                bestRate += _visitables[currNode].Rate;

            return (bestRate, bestPath);
        }
    }
}