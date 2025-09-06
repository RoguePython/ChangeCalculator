using ChangeCalculator.Core.Models;

namespace ChangeCalculator.Core.Services;

public interface IChangeCalculatorService
{
    ChangeResult Calculate(decimal amount);
}

public sealed class ChangeCalculatorService : IChangeCalculatorService
{
    // Work in cents to avoid decimal precision problems
    private static readonly (string Label, int Cents)[] Denominations =
    {
        ("R200", 20000),
        ("R100", 10000),
        ("R50",  5000),
        ("R20",  2000),
        ("R10",  1000),
        ("R5",    500),
        ("R2",    200),
        ("R1",    100),
        ("50c",    50),
        ("20c",    20),
        ("10c",    10)
    };

    public ChangeResult Calculate(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be non-negative.");

        // Round to 2 decimals (banker’s rounding) and convert to cents
        var cents = (int)Math.Round(amount * 100m, MidpointRounding.AwayFromZero);

        // Smallest denomination is 10c — if remainder < 10 cents, reject as not exactly representable
        var remainder = cents % 10;
        if (remainder != 0)
            throw new ArgumentException("Amount must be representable using denominations down to 10 cents.", nameof(amount));

        var result = new ChangeResult();
        foreach (var (label, denomCents) in Denominations)
        {
            var count = cents / denomCents;
            result[label] = count;
            cents -= count * denomCents;
        }

        return result;
    }
}
