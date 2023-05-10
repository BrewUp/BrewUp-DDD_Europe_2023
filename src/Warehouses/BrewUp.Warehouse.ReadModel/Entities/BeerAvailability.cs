namespace BrewUp.Warehouse.ReadModel.Entities;

public class BeerAvailability : EntityBase
{
    public string BeerId { get; set; } = string.Empty;

    public string BeerName { get; set; } = string.Empty;

    public string Availability { get; set; } = string.Empty;
}