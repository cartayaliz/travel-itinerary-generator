using Turist.Domain.Graph;
using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Services;

public interface ITransportWebService
{
    Task LoadAsync();

    int[] ProposeAlternatives(
        int num,
        int originId,
        int destinationId,
        Weight maxWeight,
        Func<Parada, bool> filterAct);
}