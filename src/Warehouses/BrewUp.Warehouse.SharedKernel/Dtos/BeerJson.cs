namespace BrewUp.Warehouse.SharedKernel.Dtos;

public class BeerJson
{
	public string BeerId { get; set; } = string.Empty;
	public string BeerName { get; set; } = string.Empty;
	public double Stock { get; set; } = 0;
	public double Availability { get; set; } = 0;
}