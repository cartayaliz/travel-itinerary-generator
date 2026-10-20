namespace Turist.Domain.Interfaces;
/// <summary>
/// Representa un conjunto de nodos, sin aristas.
/// </summary>
public interface INodeSet
{
    /// <summary>
    /// Devuelve la cantidad de nodos.
    /// </summary>
    /// <returns></returns>
    int NodesCount { get; }

    /// <summary>
    /// Devuelve el conjunto de nodos.
    /// </summary>
    /// <returns></returns>
    IEnumerable<int> Nodes { get; }

    /// <summary>
    /// Añade un nodo.
    /// </summary>
    /// <param name="node"></param>
    int AddNode();

    /// <summary>
    /// Elimina un nodo y sus aristas incidentes.
    /// Devuelve:
    ///     - true: si la operación es existosa.
    ///     - false: caso contrario
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    bool RemoveNode(int node);

    /// <summary>
    /// Devuelve:
    ///     - true: si el nodo existe.
    ///     - false: caso contrario.  
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    bool HasNode(int node);

    static List<int> GetPath(int[] pi, int from, int to)
    {
        List<int> ints = [];
        var temp = to;
        ints.Add(temp);
        while (pi[temp] != int.MaxValue)
        {
            ints.Add(pi[temp]);
            temp = pi[temp];
        }
        ints.Reverse();
        return ints;
    }
}

/// <summary>
/// Representa un grafo ponderado dirigido.
/// </summary>
public interface IWeightedGraph<TWeight> : INodeSet
{
    /// <summary>
    /// Devuelve la cantidad de aristas. 
    /// </summary>
    /// <returns></returns>
    int EdgesCount { get; }
    /// <summary>
    /// Devuelve el conjunto de aristas.
    /// </summary>
    /// <returns></returns>
    IEnumerable<(int from, int to, TWeight weight)> Edges { get; }
    /// <summary>
    /// Devuelve los vecinos. 
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    IEnumerable<(int to, TWeight weight)> GetNeighbors(int from);
    /// <summary>
    /// Añade arista con peso.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="weight"></param>
    void AddEdge(int from, int to, TWeight weight);

    /// <summary>
    /// Elimina arista.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    bool RemoveEdge(int from, int to);
    /// <summary>
    /// Devuelve:
    ///     - true: si la arista existe;
    ///     - false: caso contrario.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    bool HasEdge(int from, int to);
    /// <summary>
    /// Devuelve el peso de una arista dada.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    TWeight? GetWeight(int from, int to);
    /// <summary>
    /// Devuelve:
    ///     - true: si la arista existe. Variable de salida es asignada el peso de la arista hallada.
    ///     - false: caso contrario. Variable de salida es asignada NaN.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    bool TryGetWeight(int from, int to, out TWeight? weight);
    /// <summary>
    /// Asigna el peso a una arista dada.
    /// Devuelve:
    ///     - true, si la operación fue existosa.
    ///     - false, caso contrario. 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="newWeight"></param>
    /// <returns></returns>
    bool SetWeight(int from, int to, TWeight newWeight);
    /// <summary>
    /// Árbol de rutas mínimas desde un nodo de origen usando el algoritmo de Dijkstra.
    /// El comparador debe definir un orden compatible con la suma de pesos y los pesos deben ser no negativos.
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    (int[] parents, TWeight[] distances, bool[] hasDistance) DijkstraGetMst(int from, Func<TWeight, TWeight, int> compareFunc);
}
/// <summary>
/// Representa un grafo no ponderado.
/// </summary>
public interface IUnweightedGraph : INodeSet
{
    /// <summary>
    /// Devuelve el conjunto de aristas.
    /// </summary>
    /// <returns></returns>
    IEnumerable<(int from, int To)> Edges { get; }
    /// <summary>
    /// Devuelve los vecinos.
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    IEnumerable<int> GetNeighbors(int from);
    /// <summary>
    /// Añade arista.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    void AddEdge(int from, int to);
    /// <summary>
    /// Elimina arista.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    bool RemoveEdge(int from, int to);
    /// <summary>
    /// Devuelve:
    ///     - true: si la arista existe;
    ///     - false: caso contario.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    bool HasEdge(int from, int to);
}