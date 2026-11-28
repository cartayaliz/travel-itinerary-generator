using Turist.Domain.Graph;
using Turist.Domain.Models;

namespace Turist.Services.Interfaces;

public interface ITransportWebService
{
    Task LoadAsync();
    List<int> ProposeAlternatives(
        int maxStopsCnt,
        int originId,
        int destinationId,
        Weight maxWeight,
        Func<Parada, bool> filterStop);
    bool TryGetParada(int paradaId, out Parada parada);
    bool TryGetShortestWeight(int fromParadaId, int toParadaId, out Weight weight);
    IEnumerable<int> GetGraphNodeIds();
}