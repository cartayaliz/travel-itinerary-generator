using Turist.Domain.Graph;
using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Services;

public interface ITransportWebService
{
    Task LoadAsync();
    List<int> ProposeAlternatives(
        int maxStopsCnt,
        int originId,
        int destinationId,
        Weight maxWeight,
        Func<Parada, bool> filterStop);

}