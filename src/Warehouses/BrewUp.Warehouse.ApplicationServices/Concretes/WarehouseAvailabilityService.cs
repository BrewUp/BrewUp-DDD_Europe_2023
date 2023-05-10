using BrewUp.Warehouse.ApplicationServices.Abstracts;
using BrewUp.Warehouse.ApplicationServices.DTOs;
using BrewUp.Warehouse.ReadModel;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ApplicationServices.Concretes;

internal class WarehouseAvailabilityService : WarehouseBaseService, IWarehouseAvailabilityService
{
    public WarehouseAvailabilityService(ILoggerFactory loggerFactory, IPersister persister) : base(loggerFactory, persister)
    {
        
    }
    public Task<BeerAvailabilityModelBase> GetBeerAvailabilityAsync(CancellationToken cancellationToken)
    {
        
        
        return Task.FromResult(new BeerAvailabilityModelBase
        {
            BeerId = Guid.NewGuid().ToString(),
            BeerName = "Muflone IPA",
            Availability = "100l"
        });
    }
}
