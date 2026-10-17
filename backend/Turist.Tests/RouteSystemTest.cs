using Turist.Domain.Interfaces;
using Turist.Infrastructure.Services;
using Turist.Tests.Fakes;
using Turist.Tests.Helpers;
using Xunit;

namespace Turist.Tests;

public class RouteSystemTest
{
    private static FakeVisitable Lugar(
        int id,
        int visitMinutes = 10,
        DateTime? abre = null,
        DateTime? cierra = null)
    {
        return new FakeVisitable
        {
            Id = id,
            Schedule = new FakeSchedule
            {
                OpensAt = abre,
                ClosesAt = cierra,
                VisitTimeSpan = TimeSpan.FromMinutes(visitMinutes),
            }
        };
    }
    
    private static FakeTransportation Arista(int from, int to, int travelMinutes)
    {
        return new FakeTransportation
        {
            StartVisitableId = from,
            StopVisitableId = to,
            TravelTimeSpan = new TimeSpan(travelMinutes),
            Cost = 0,
        };
    }

    private static readonly DateTime Inicio = new DateTime(2025, 1, 15, 9, 0, 0);
    private static readonly DateTime Fin = new DateTime(2025, 1, 15, 18, 0, 0);

    
}