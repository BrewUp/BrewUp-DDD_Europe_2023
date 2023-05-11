namespace BrewUp.Warehouse.ReadModel.Entities;

public class BeerAvailability : EntityBase
{
	public string BeerName { get; private set; } = string.Empty;
	public double Stock { get; private set; } = 0;
	public double Availability { get; private set; } = 0;

	protected BeerAvailability()
	{ }

	public static BeerAvailability Create(string beerName, double stock, double availability)
	{
		return new BeerAvailability(beerName, stock, availability);
	}

	private BeerAvailability(string beerName, double stock, double availability)
	{
		BeerName = beerName;
		Stock = stock;
		Availability = availability;
	}
}