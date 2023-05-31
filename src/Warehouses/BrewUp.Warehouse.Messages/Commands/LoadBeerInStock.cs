using BrewUp.Warehouse.SharedKernel.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUp.Warehouse.Messages.Commands;

public sealed class LoadBeerInStock : Command
{
	public readonly BeerId BeerId;
	public readonly Stock Stock;
	public readonly PurchaseOrderId PurchaseOrderId;

	public LoadBeerInStock(BeerId aggregateId, Stock stock, PurchaseOrderId purchaseOrderId) : base(aggregateId)
	{
		BeerId = aggregateId;
		Stock = stock;
		PurchaseOrderId = purchaseOrderId;
	}
}