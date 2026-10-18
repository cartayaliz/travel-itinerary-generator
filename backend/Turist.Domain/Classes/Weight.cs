using System.Numerics;

namespace Turist.Domain.Classes;

public readonly record struct Weight(TimeSpan Time, decimal Cost) : IComparable<Weight>, 
                                                                    IAdditiveIdentity<Weight, Weight>, 
                                                                    IAdditionOperators<Weight, Weight, Weight>
{
    public static Weight AdditiveIdentity => new(TimeSpan.Zero, 0);

    public int CompareTo(Weight other)
    {
        var timeComparison = Time.CompareTo(other.Time);
        var costComparison = Cost.CompareTo(other.Cost);
        return costComparison == 0 ? timeComparison : costComparison;
    }

    public static Weight operator +(Weight left, Weight right) =>
        new(left.Time + right.Time, left.Cost + right.Cost);

    public static bool operator <(Weight left, Weight right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Weight left, Weight right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Weight left, Weight right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Weight left, Weight right)
    {
        return left.CompareTo(right) >= 0;
    }
}