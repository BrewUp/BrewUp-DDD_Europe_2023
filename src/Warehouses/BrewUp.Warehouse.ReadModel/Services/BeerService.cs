using BrewUp.Warehouse.ReadModel.Entities;
using BrewUp.Warehouse.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.Services;

public class BeerService : WarehouseBaseService, IBeerService
{
	public BeerService(ILoggerFactory loggerFactory, IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task<BeerId> AddBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();

		try
		{
			var beer = await Persister.GetByIdAsync<Beer>(beerId.ToString(), cancellationToken);
			if (beer != null)
				return new BeerId(new Guid(beer.Id));

			beer = Beer.Create(beerId, beerName);
			await Persister.InsertAsync(beer, cancellationToken);

			return new BeerId(new Guid(beer.Id));
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}