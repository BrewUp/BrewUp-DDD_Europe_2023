using BrewUp.Warehouse.ReadModel;
using BrewUp.Warehouse.ReadModel.DTOs;

namespace BrewUp.Warehouse.ApplicationServices.DTOs;

public class BeerAvailabilityModelBase : ModelBase
{
    public string BeerId { get; set; } = string.Empty;
    
    public string BeerName { get; set; } = string.Empty;
    
    public string Availability { get; set; } = string.Empty;
}