namespace FleetManagement.Equipment.Domain.ValueObjects
{
  public sealed record Money
  {
    public decimal Amount { get; set; }
    public required string Concurrency { get; set; }

    public Money(decimal amount, string concurrency)
    {
      if (amount < 0)
        throw new ArgumentException("Amount can't be negative", nameof(amount));
      if (string.IsNullOrWhiteSpace(concurrency))
        throw new ArgumentException("Concurrency must be set", nameof(concurrency));

      Amount = amount;
      Concurrency = concurrency;
    }
  }
}
