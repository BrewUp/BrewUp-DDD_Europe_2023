using BrewUp.Warehouse.ReadModel.DTOs;

namespace BrewUp.Warehouse.ReadModel.Models;

public class WarehouseAvailability : ModelBase
{
    public string Name { get; private set; } = string.Empty;

    public double Stock { get; private set; } = 0;
    
    protected WarehouseAvailability()
    {
        
    }
}