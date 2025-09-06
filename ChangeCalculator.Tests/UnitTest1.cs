using ChangeCalculator.Core.Services;
using Xunit;

namespace ChangeCalculator.Tests;

public class ChangeCalculatorServiceTests
{
    private readonly ChangeCalculatorService _svc = new();

    [Fact]
    public void Calculate_Example_786_80()
    {
        var result = _svc.Calculate(786.80m);

        Assert.Equal(3, result["R200"]);
        Assert.Equal(1, result["R100"]);
        Assert.Equal(1, result["R50"]);
        Assert.Equal(1, result["R20"]);
        Assert.Equal(1, result["R10"]);
        Assert.Equal(1, result["R5"]);
        Assert.Equal(0, result["R2"]);
        Assert.Equal(1, result["R1"]);
        Assert.Equal(1, result["50c"]);
        Assert.Equal(1, result["20c"]);
        Assert.Equal(1, result["10c"]);
    }

    [Theory]
    [InlineData(-1)]
    public void Calculate_Negative_Throws(decimal amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _svc.Calculate(amount));
    }

    [Theory]
    [InlineData(0.01)]
    [InlineData(0.02)]
    [InlineData(0.03)]
    [InlineData(0.04)]
    [InlineData(0.05)]
    [InlineData(0.06)]
    [InlineData(0.07)]
    [InlineData(0.08)]
    [InlineData(0.09)]
    public void Calculate_NotRepresentable_Throws(decimal amount)
    {
        Assert.Throws<ArgumentException>(() => _svc.Calculate((decimal)amount));
    }
}
