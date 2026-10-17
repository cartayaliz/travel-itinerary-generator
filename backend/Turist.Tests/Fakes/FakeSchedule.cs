using Turist.Domain.Interfaces;

namespace Turist.Tests.Fakes;

/// <summary>
/// Horario configurable. Si no se especifica <see cref="OpensAt"/> ni
/// <see cref="ClosesAt"/>, el lugar se considera siempre abierto.
/// </summary>
public class FakeSchedule : ISchedule
{
    public DateTime? OpensAt { get; init; }
    public DateTime? ClosesAt { get; init; }
    public TimeSpan VisitTimeSpan { get; init; } = TimeSpan.FromMinutes(10);

    public bool IsOpen(DateTime since, DateTime until)
    {
        if (OpensAt is null || ClosesAt is null) return true;
        var apertura = since.Date + OpensAt.Value.TimeOfDay;
        var cierre = since.Date + ClosesAt.Value.TimeOfDay;
        return since >= apertura && until <= cierre;
    }

    public bool IsOpen(DateTime now)
    {
        if (OpensAt is null || ClosesAt is null) return true;
        var hora = now.TimeOfDay;
        return hora >= OpensAt.Value.TimeOfDay && hora < ClosesAt.Value.TimeOfDay;
    }
}