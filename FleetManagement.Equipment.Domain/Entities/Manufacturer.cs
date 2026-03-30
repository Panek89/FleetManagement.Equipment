namespace FleetManagement.Equipment.Domain.Entities;

public class Manufacturer : BaseEntity
{
  private string _name = null!;
  private string _country = null!;

  public string Name
  {
    get => _name;
    set
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Name must be set", nameof(Name));
      _name = value;
    }
  }

  public string Country
  {
    get => _country;
    set
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Country must be set", nameof(Country));
      _country = value;
    }
  }

  public ICollection<Car> Cars { get; set; } = [];

  private Manufacturer() { }

  public Manufacturer(string name, string country)
  {
    Name = name;
    Country = country;
  }

  public Manufacturer(string name, string country, bool isActive, string createdBy)
      : this(name, country)
  {
    IsActive = isActive;
    CreatedBy = createdBy;
  }
}
