using FleetManagement.Equipment.Domain.ValueObjects;

namespace FleetManagement.Equipment.Domain.Entities;

public class BaseEquipment<T> : BaseEntity<T> where T : BaseEquipment<T>
{
  public Money InitialValue { get; set; }
  public Money CurrentValue { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }

  public BaseEquipment(Money initialValue, Money currentValue, string title, string description)
    : base()
  {
    InitialValue = initialValue;
    CurrentValue = currentValue;
    Title = title;
    Description = description;
  }

  public BaseEquipment(bool isActive, string createdBy, Money initialValue, Money currentValue, string title, string description)
   : base(isActive, createdBy)
  {
    InitialValue = initialValue;
    CurrentValue = currentValue;
    Title = title;
    Description = description;
  }

  public void DecreaseValueByPercentage(Percentage percentage)
  {
    var amount = CurrentValue.Amount * percentage.Percent / 100m;
    DecreaseValueByAmount(amount);
  }

  public void DecreaseValueByAmount(decimal amount)
  {
    CurrentValue = new Money(CurrentValue.Amount - amount, CurrentValue.Currency);
  }

  public void Revaluate(decimal value)
  {
    CurrentValue = new Money(value, CurrentValue.Currency);
  }
}
