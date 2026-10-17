using Turist.Domain.Interfaces;

namespace Turist.Tests.Fakes;

public class FakeVisitable : IVisitable
{
    public int Id { get; init; }
    public ISchedule Schedule { get; init; } = new FakeSchedule();
    public int CategoryId { get; init; } = 1;
    public double Rate { get; init; } = 5.0;
    public bool IsBlocked { get; set; } = false;
}