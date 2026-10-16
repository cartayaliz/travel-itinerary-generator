using Turist.Domain.Interfaces;

namespace Turist.Tests.Fakes;

public class FakeSchedule : ISchedule
{
    /// <summary>Hora de apertura diaria. Si es null, no hay restricción.</summary>
    public DateTime? OpensAt { get; init; }

    /// <summary>Hora de cierre diaria. Si es null, no hay restricción.</summary>
    public DateTime? ClosesAt { get; init; }

    /// <summary>Duración mínima de la visita. Por defecto, 30 minutos.</summary>
    public TimeSpan VisitTimeSpan { get; init; } = TimeSpan.FromMinutes(30);

    /// <summary>
    /// ¿Está abierto durante todo el intervalo [since, until]?
    /// Si no hay horas definidas, siempre está abierto.
    /// </summary>
    public bool IsOpen(DateTime since, DateTime until)
    {
        if (OpensAt is null && ClosesAt is null) return true;
        if (OpensAt is null || ClosesAt is null) return true;

        // Ajustar al día del intervalo: usamos la hora del día
        var apertura = new DateTime(since.Year, since.Month, since.Day)
            .Add(OpensAt.Value.TimeOfDay);
        var cierre = new DateTime(since.Year, since.Month, since.Day)
            .Add(ClosesAt.Value.TimeOfDay);

        // El intervalo completo debe caer dentro de [apertura, cierre]
        return since >= apertura && until <= cierre;
    }

    /// <summary>¿Está abierto en el instante <paramref name="now"/>?</summary>
    public bool IsOpen(DateTime now)
    {
        if (OpensAt is null && ClosesAt is null) return true;
        if (OpensAt is null || ClosesAt is null) return true;

        var hora = now.TimeOfDay;
        return hora >= OpensAt.Value.TimeOfDay && hora < ClosesAt.Value.TimeOfDay;
    }
}

public class FakeTransportation : ITransportation
{
    public int StartVisitableId { get; init; }
    public int StopVisitableId { get; init; }
    public TimeSpan TravelTimeSpan { get; init; }
    public double TotalCost { get; init; }
}

public class FakeVisitable : IVisitable
{
    public int Id { get; init; }

    /// <summary>
    /// Estado mutable: cambia cuando el algoritmo lo visita.
    /// Los tests pueden leerlo al final para verificar el estado.
    /// </summary>
    public bool IsBlocked { get; set; }

    public ISchedule Schedule { get; init; } = new FakeSchedule();
}