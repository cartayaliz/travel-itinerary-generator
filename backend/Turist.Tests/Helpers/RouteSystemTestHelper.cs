using Turist.Infrastructure.Services;
using Turist.Tests.Fakes;

namespace Turist.Tests.Helpers;

/// <summary>
/// Utilidades compartidas por los tests de <c>RouteSystem</c>.
/// Cada servicio bajo prueba tiene su propio helper.
/// </summary>
public static class RouteSystemTestHelper
{
    // Fecha base para todos los tests: 2025-01-15
    public static readonly DateTime Day = new(2025, 1, 15);

    public static DateTime Hour(int h) => Day.AddHours(h);

    public static FakeVisitable Lugar(
        int id,
        double rate = 5,
        int categoryId = 1,
        Domain.Interfaces.ISchedule? schedule = null,
        bool blocked = false)
        => new()
        {
            Id = id,
            Rate = rate,
            CategoryId = categoryId,
            Schedule = schedule ?? new FakeSchedule(),
            IsBlocked = blocked,
        };

    public static FakeTransportation T(int from, int to, int minutes, double cost = 0)
        => new()
        {
            StartVisitableId = from,
            StopVisitableId = to,
            TravelTimeSpan = TimeSpan.FromMinutes(minutes),
            Cost = cost,
        };

    public static RouteSystem Sistema(
        IEnumerable<FakeVisitable> visitables,
        IEnumerable<FakeTransportation> transportes)
        => new(visitables, transportes);

    public static int[] Ids(Route route)
        => [.. route.visitablesId.Select(v => v.Id)];

    public static Dictionary<int, bool> SinFiltro() => [];
}