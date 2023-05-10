using BrewUp.Warehouse.ReadModel;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ApplicationServices.Abstracts;

public abstract class WarehouseBaseService
{
    protected ILogger Logger;

    protected IPersister Persister;

    protected WarehouseBaseService(ILoggerFactory loggerFactory, IPersister persister)
    {
        Logger = loggerFactory.CreateLogger(GetType());
        Persister = persister;
    }
}