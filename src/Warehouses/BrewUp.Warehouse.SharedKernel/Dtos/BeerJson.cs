namespace BrewUp.Warehouse.SharedKernel.Dtos;

public class BeerJson
{
	public string BeerId { get; set; } = string.Empty;
	public string BeerName { get; set; } = string.Empty;
	public decimal Stock { get; set; } = 0;
	public decimal Availability { get; set; } = 0;
}