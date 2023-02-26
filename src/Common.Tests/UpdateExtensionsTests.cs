using Xunit;

namespace RidingBikes.Common.Tests;

public class UpdateExtensionsTests
{
    [Theory]
    [InlineData(0, 0, false)]
    [InlineData(double.NaN, double.NaN, false)]
    [InlineData(0, double.NaN, false)]
    [InlineData(double.NaN, 0, true)]
    [InlineData(double.NegativeInfinity, 1, true)]
    [InlineData(42.01, double.PositiveInfinity, true)]
    [InlineData(double.PositiveInfinity, double.PositiveInfinity, false)]
    public void DoubleUpdate(double first, double second, bool expected)
    {
        var actual = first.CheckForUpdate(second);
        Assert.Equal(expected, actual);
    }
}