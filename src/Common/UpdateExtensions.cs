using RidingBikes.Common.Models;

namespace RidingBikes.Common;

public static class UpdateExtensions
{
    public static bool CheckForUpdate(this double value, double other)
    {
        if (double.IsNaN(other)) return false;

        if (double.IsNaN(value)) return true;

        return value != other;
    }

    public static bool CheckForUpdate(this string value, string? other)
    {
        if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(other)) return false;
        if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(other)) return true;
        return !string.Equals(value, other);
    }

    public static bool CheckForUpdate(this DateTime value, DateTime other)
    {
        return value != other;
    }

    public static bool CheckForUpdate(this TimeOnly value, TimeOnly other)
    {
        return value != other;
    }

    public static bool CheckForUpdate(this DateOnly value, DateOnly other)
    {
        return value != other;
    }
    public static bool CheckForUpdate(this Guid value, Guid other)
    {
        if (other == Guid.Empty) return false;
        if (value == Guid.Empty) return true;

        return !Guid.Equals(value, other);
    }

    public static bool CheckForUpdate(this RideType value, RideType other)
    {
        return value != other;
    }
}