using System.Numerics;

namespace Turist.Domain.Classes;

public readonly record struct Weight(TimeSpan Time, double Cost) : IAdditiveIdentity<Weight, Weight>,
                                                                    IAdditionOperators<Weight, Weight, Weight>
{
    public static Weight AdditiveIdentity => new(TimeSpan.Zero, 0);

    public static double Score(
        Weight weight,
        TimeSpan maxTime,
        double maxCost,
        double timeShare = 0.5) //constante alpha
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxTime, TimeSpan.Zero);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxCost);
        if (timeShare is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(timeShare));
        double c = weight.Cost; 
        double t = weight.Time.TotalMinutes;
        return timeShare * (t * maxCost - c * maxTime.TotalMinutes) + c * maxTime.TotalMinutes;
    }

    public static int Compare(
        Weight left,
        Weight right,
        TimeSpan maxTime,
        double maxCost,
        double timeShare = 0.5)
    {
        var scoreComparison = Score(left, maxTime, maxCost, timeShare)
            .CompareTo(Score(right, maxTime, maxCost, timeShare));
        if (scoreComparison != 0)
            return scoreComparison;

        var costComparison = left.Cost.CompareTo(right.Cost);
        return costComparison != 0
            ? costComparison
            : left.Time.CompareTo(right.Time);
    }

    public static Weight operator +(Weight left, Weight right)
        => new(left.Time + right.Time, left.Cost + right.Cost);
}
