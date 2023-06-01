﻿using BrewUp.Warehouse.SharedKernel.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Messages.Events;

public sealed class BeerLoadedInStock : DomainEvent
{
	public readonly BeerId BeerId;
	public readonly Stock Stock;

	public readonly PurchaseOrderId PurchaseOrderId;

	public BeerLoadedInStock(BeerId aggregateId, Guid correlationId, Stock stock, PurchaseOrderId purchaseOrderId) : base(aggregateId, correlationId)
	{
		BeerId = aggregateId;
		Stock = stock;
		PurchaseOrderId = purchaseOrderId;
	}
}