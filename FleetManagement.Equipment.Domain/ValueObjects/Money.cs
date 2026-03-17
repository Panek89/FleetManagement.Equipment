namespace FleetManagement.Equipment.Domain.ValueObjects;

public sealed record Money
{
  public decimal Amount { get; set; }
  public string Currency { get; set; }

  public Money(decimal amount, string currency)
  {
    if (amount < 0)
      throw new ArgumentException("Amount can't be negative", nameof(amount));
    if (string.IsNullOrWhiteSpace(currency))
      throw new ArgumentException("Concurrency must be set", nameof(currency));

    Amount = amount;
    Currency = currency;
  }
}
