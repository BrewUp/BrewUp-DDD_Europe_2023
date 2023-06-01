using BrewUp.Warehouse.SharedKernel.Dtos;

namespace BrewUp.Warehouses.Sagas.Sagas;

public class BeersReceivedSagaState
{
	public string PurchaseOrderId { get; set; } = string.Empty;

	public IEnumerable<OrderLine> OrderLines { get; set; } = Enumerable.Empty<OrderLine>();

	public DateTime StartedAt { get; set; } = DateTime.MinValue;
	public DateTime FinishedAt { get; set; } = DateTime.MinValue;
}