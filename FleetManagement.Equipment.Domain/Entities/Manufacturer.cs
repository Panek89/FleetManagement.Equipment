namespace FleetManagement.Equipment.Domain.Entities;

public class Manufacturer : BaseEntity<Manufacturer>
{
  public string Name { get; set; }
  public string Country { get; set; }

  public ICollection<Car> Cars { get; set; } = [];

  public Manufacturer(string name, string country)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Name must be set", nameof(name));

    if (string.IsNullOrWhiteSpace(country))
      throw new ArgumentException("Country must be set", nameof(country));

    Name = name;
    Country = country;
  }
}
