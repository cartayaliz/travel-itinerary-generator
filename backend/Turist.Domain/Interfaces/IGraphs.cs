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
    int GetNodesCount();

    /// <summary>
    /// Devuelve el conjunto de nodos.
    /// </summary>
    /// <returns></returns>
    IEnumerable<int> GetNodes();

    /// <summary>
    /// Añade un nodo.
    /// </summary>
    /// <param name="node"></param>
    void AddNode(int node);

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
}

/// <summary>
/// Representa un grafo ponderado dirigido.
/// </summary>
public interface IWeightedGraph : INodeSet
{
    /// <summary>
    /// Devuelve la cantidad de aristas. 
    /// </summary>
    /// <returns></returns>
    int GetEdgesCount();

    /// <summary>
    /// Devuelve el conjunto de aristas.
    /// </summary>
    /// <returns></returns>
    IEnumerable<(int from, int to, double weight)> GetEdges();

    /// <summary>
    /// Devuelve los vecinos. 
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    IEnumerable<(int to, double weight)> GetNeighbors(int from);

    /// <summary>
    /// Añade una arista con peso.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="weight"></param>
    void AddEdge(int from, int to, double weight);

    /// <summary>
    /// Elimina una arista.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    int RemoveEdge(int from, int to);

    /// <summary>
    /// Devuelve:
    ///     - true: si la arista existe.
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
    double GetWeight(int from, int to);

    /// <summary>
    /// Devuelve:
    ///     - true: si la arista existe. Variable de salida es asignada el peso de la arista hallada.
    ///     - false: caso contrario. Variable de salida es asignada NaN.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    bool TryGetWeight(int from, int to, out double weight);
    
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
    bool SetWeight(int from, int to, double newWeight);
}

// public interface IWeightlessGraph : INodeSet
// {
//     IEnumerable<(int from, int To)> GetEdges();
//     IEnumerable<int> GetNeighbors(int from);
//     void AddEdge(int from, int to);
//     void RemoveEdge(int from, int to);
//     bool HasEdge(int from, int to);
// }