namespace BrewUp.Warehouse.ApplicationServices.DTOs;

public class BeerAvailabilityDTO
{
    public string BeerId { get; set; } = string.Empty;
    
    public string BeerName { get; set; } = string.Empty;
    
    public string Availability { get; set; } = string.Empty;
}