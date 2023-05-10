namespace BrewUp.Warehouse.ReadModel.Entities;

public class WarehouseAvailability : EntityBase
{
	public string Name { get; private set; } = string.Empty;

	public double Stock { get; private set; } = 0;

	protected WarehouseAvailability()
	{

	}
}