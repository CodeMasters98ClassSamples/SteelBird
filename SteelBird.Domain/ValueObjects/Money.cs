namespace SteelBird.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    public Money(decimal amount, string currency) { Amount = amount; Currency = currency; }

    public static Money Zero(string c) => new(0, c);
    public static Money Of(decimal amount, string c) => new(amount, c);
    public Money Add(Money other) { EnsureSameCcy(other); return new(Amount + other.Amount, Currency); }
    public Money Multiply(int qty) => new(Amount * qty, Currency);
    private void EnsureSameCcy(Money other) { if (Currency != other.Currency) throw new Exception("Currency mismatch"); }
}

