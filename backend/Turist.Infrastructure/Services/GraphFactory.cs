using Turist.Domain.Interfaces;
using Turist.Domain.Classes;

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
        foreach (var transp in transpWeb)
        {
            double weight = transp.TotalCost + transp.TotalTime;;
            
            int n1 = GetNode(transp.StartLocationId);
            if (!graph.HasNode(n1)) continue;

            int n2 = GetNode(transp.StopLocationId);
            if (!graph.HasNode(n1)) continue;
            
            graph.AddEdge(n1, n2, weight);
        }
        return graph;
    }
}