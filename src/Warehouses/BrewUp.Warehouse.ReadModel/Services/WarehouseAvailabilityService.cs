using BrewUp.Warehouse.ReadModel.Entities;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.Services;

public class WarehouseAvailabilityService : WarehouseBaseService, IWarehouseAvailabilityService
{
	public WarehouseAvailabilityService(ILoggerFactory loggerFactory, IPersister persister) : base(loggerFactory, persister)
	{

	}
	public Task<BeerAvailability> GetBeerAvailabilityAsync(CancellationToken cancellationToken)
	{


		return Task.FromResult(new BeerAvailability
		{
			BeerId = Guid.NewGuid().ToString(),
			BeerName = "Muflone IPA",
			Availability = "100l"
		});
	}
}
