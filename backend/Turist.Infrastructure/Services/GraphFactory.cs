using Turist.Domain;
using Turist.Domain.Interfaces;

namespace Turist.Infrastructure.Services;

/// <summary>
/// Contruye y gestiona un grafo dado el conjunto de actividades y atracciones (localizaciones).
/// </summary>
public class GraphFactory
{
    private readonly Dictionary<int, IVisitableLocation> _nodeVisitLocDict;

    public GraphFactory()
    {
        _nodeVisitLocDict = [];
    }

    /// <summary>
    /// Dado un nodo devuelve la localización que representa en el grafo.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public IVisitableLocation GetLocation(int node) => _nodeVisitLocDict[node];

    /// <summary>
    /// Función de peso para las aristas del grafo. Recibe la red de transporte disponible de un nodo a otro.
    /// </summary>
    /// <param name="transp"></param>
    /// <returns></returns>
    private double WeightFunction(ITransportation transp)
    {
        return transp.TotalCost + transp.TotalTime;
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
    /// <param name="nodeId"></param>
    /// <returns></returns>
    public int GetLocationId(int nodeId) => nodeId;

    /// <summary>
    /// Construye el grafo dado las actividades y atracciones, las relacionan entre sí según la red de transporte disponible.
    /// </summary>
    /// <param name="activities"></param>
    /// <param name="attractions"></param>
    /// <param name="transpWeb"></param>
    /// <returns></returns>
    public IWeightedGraph Build(IEnumerable<IVisitableLocation> activities,
                                IEnumerable<IVisitableLocation> attractions,
                                IEnumerable<ITransportation> transpWeb)
    {
        var graph = new WeightedGraph();
        foreach (var activity in activities)
        {
            int node = GetNode(activity.Id);
            _nodeVisitLocDict[node] = activity;
            graph.AddNode(node);
        }
        foreach (var attraction in attractions)
        {
            int node = GetNode(attraction.Id);
            _nodeVisitLocDict[node] = attraction;
            graph.AddNode(node);
        }
        foreach (var availableTransp in transpWeb)
        {
            double weight = WeightFunction(availableTransp);
            
            int n1 = GetNode(availableTransp.StartLocationId);
            if (!graph.HasNode(n1)) continue;

            int n2 = GetNode(availableTransp.StopLocationId);
            if (!graph.HasNode(n1)) continue;
            
            graph.AddEdge(n1, n2, weight);
        }
        return graph;
    }
}