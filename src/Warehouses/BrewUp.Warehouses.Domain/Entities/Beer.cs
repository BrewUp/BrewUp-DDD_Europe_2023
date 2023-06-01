using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.SharedKernel.DomainIds;
using Muflone.Core;

namespace BrewUp.Warehouses.Domain.Entities;

public sealed class Beer : AggregateRoot
{
	private BeerName _beerName;

	private IEnumerable<StockMovement> _movements;

	private Stock _stock;

	protected Beer()
	{ }

	internal static Beer CreateBeer(BeerId beerId, BeerName beerName, Guid correlationId)
	{
		return new Beer(beerId, beerName, correlationId);
	}

	private Beer(BeerId beerId, BeerName beerName, Guid correlationId)
	{
		RaiseEvent(new BeerCreated(beerId, correlationId, beerName));
	}

	private void Apply(BeerCreated @event)
	{
		Id = @event.BeerId;
		_beerName = @event.BeerName;

		_movements = Enumerable.Empty<StockMovement>();
		_stock = new Stock(0);
	}

	#region LoadInStock
	internal void LoadBeerInStock(BeerId beerId, Stock stock, PurchaseOrderId purchaseOrderId, Guid correlationId)
	{
		var movement = _movements.FirstOrDefault(m => m.PurchaseOrderId == purchaseOrderId);
		if (movement is not null)
			return;

		var stockUpdated = new Stock(_stock.Value + stock.Value);

		RaiseEvent(new BeerLoadedInStock(beerId, correlationId, stockUpdated, purchaseOrderId));
	}

	private void Apply(BeerLoadedInStock @event)
	{
		_movements = _movements.Append(new StockMovement(@event.PurchaseOrderId, @event.BeerId,
			new Stock(@event.Stock.Value - _stock.Value)));

		_stock = @event.Stock;
	}
	#endregion
}