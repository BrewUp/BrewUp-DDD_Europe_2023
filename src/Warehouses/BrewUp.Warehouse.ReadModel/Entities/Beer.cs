using BrewUp.Warehouse.SharedKernel.Dtos;

namespace BrewUp.Warehouse.ReadModel.Entities;

public class Beer : EntityBase
{
	public string BeerName { get; private set; } = string.Empty;
	public double Stock { get; private set; } = 0;
	public double Availability { get; private set; } = 0;

	protected Beer()
	{ }

	public static Beer Create(string beerName, double stock, double availability)
	{
		return new Beer(beerName, stock, availability);
	}

	private Beer(string beerName, double stock, double availability)
	{
		BeerName = beerName;
		Stock = stock;
		Availability = availability;
	}

	public BeerJson ToJson()
	{
		return new BeerJson
		{
			BeerId = Id,
			BeerName = BeerName,
			Stock = Stock,
			Availability = Availability
		};
	}
}