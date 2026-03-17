namespace FleetManagement.Equipment.Domain.ValueObjects;

public sealed record Percentage
{
  public decimal Percent { get; set; }

  public Percentage(decimal percent)
  {
    if (percent is < 0 or > 100)
      throw new ArgumentOutOfRangeException(nameof(percent));

    Percent = percent;
  }
}
