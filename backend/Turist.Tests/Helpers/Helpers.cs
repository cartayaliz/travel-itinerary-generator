using Turist.Domain.Classes;
using Turist.Domain.Interfaces;

namespace Turist.Tests.Helpers;

public static class GraphTestHelper
{
    public static IWeightedGraph<double> Crear(params (int from, int to, double weight)[] aristas)
    {
        var grafo = new WeightedGraph<double>();
        foreach (var (from, to, weight) in aristas)
        {
            grafo.AddNode(from);
            grafo.AddNode(to);
            grafo.AddEdge(from, to, weight);
        }
        return grafo;
    }

    /// <summary>
    /// Grafo lineal: 1 → 2 → 3 → 4, con peso 10 cada tramo.
    /// </summary>
    public static IWeightedGraph<double> Lineal() =>
        Crear((1, 2, 10), (2, 3, 10), (3, 4, 10));

    /// <summary>
    /// Grafo con dos caminos entre 1 y 4:
    ///   Camino corto:  1 → 2 → 4  (peso 5 + 5 = 10)
    ///   Camino largo:  1 → 3 → 4  (peso 20 + 20 = 40)
    /// </summary>
    public static IWeightedGraph<double> DosCaminos() =>
        Crear(
            (1, 2, 5), (2, 4, 5),
            (1, 3, 20), (3, 4, 20)
        );

    /// <summary>
    /// Dos componentes desconectadas: {1,2} y {3,4}.
    /// </summary>
    public static IWeightedGraph<double> Desconectado() =>
        Crear((1, 2, 10), (3, 4, 10));
}