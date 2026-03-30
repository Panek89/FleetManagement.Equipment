namespace FleetManagement.Equipment.Shared.Consts;

public static class DefaultValues
{
  public const string SYSTEM = nameof(SYSTEM);
  public const string SEEDER = nameof(SEEDER);

  public static IReadOnlyCollection<(string id, string name, string country)> DEFAULT_MANUFACTURERS =
  [
    ("8fcfd3c4-d9d6-4271-a443-48e6e0576b45", "BMW", "Germany"),
    ("9a0bc5a9-5868-40b2-b935-6a29c45c9603", "Renault", "France"),
    ("cdbcf3f0-59be-4984-9118-56ccbcb0047e", "Citroen", "France"),
    ("f7e9b3a4-e963-4351-9ca2-373d665f9b11", "Peugeot", "France"),
  ];
}
