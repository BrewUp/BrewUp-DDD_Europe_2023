using BrewUp.Warehouse.SharedKernel.DomainIds;
using BrewUp.Warehouse.SharedKernel.Dtos;

namespace BrewUp.Warehouse.ReadModel.Entities;

public class Beer : EntityBase
{
	public string BeerName { get; private set; } = string.Empty;
	public decimal Stock { get; private set; } = 0;
	public decimal Availability { get; private set; } = 0;

	protected Beer()
	{ }

	public static Beer Create(BeerId beerId, BeerName beerName)
	{
		return new Beer(beerId.ToString(), beerName.Value);
	}

	private Beer(string beerId, string beerName)
	{
		Id = beerId;
		BeerName = beerName;
		Stock = 0;
		Availability = 0;
	}

	public void UpdateStock(Stock stock)
	{
		Stock = stock.Value;
		Availability += Stock;
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