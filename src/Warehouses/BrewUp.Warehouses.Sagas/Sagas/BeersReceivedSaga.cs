using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace BrewUp.Warehouses.Sagas.Sagas;

public sealed class BeersReceivedSaga : Saga<BeersReceivedSagaState>,
	ICommandHandlerAsync<StartBeersReceivedSaga>,
	IDomainEventHandlerAsync<BeerCreated>,
	IDomainEventHandlerAsync<BeerLoadedInStock>
{
	public BeersReceivedSaga(IServiceBus serviceBus,
		ISagaRepository repository,
		ILoggerFactory loggerFactory) : base(serviceBus, repository, loggerFactory)
	{
	}

	public async Task HandleAsync(StartBeersReceivedSaga command, CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();

		SagaState = new BeersReceivedSagaState
		{
			PurchaseOrderId = command.PurchaseOrderId.Value.ToString(),
			OrderLines = command.OrderLines
		};
		await Repository.SaveAsync(command.MessageId, SagaState);

		foreach (var orderLine in command.OrderLines)
		{
			var createBeer = new CreateBeer(orderLine.BeerId, command.MessageId, orderLine.BeerName);
			await ServiceBus.SendAsync(createBeer, cancellationToken);
		}
	}

	public async Task HandleAsync(BeerCreated @event, CancellationToken cancellationToken = new())
	{
		var correlationId = new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		if (correlationId.Equals(Guid.Empty))
			return;

		var sagaState = await Repository.GetByIdAsync<BeersReceivedSagaState>(correlationId);
		var orderLine = sagaState.OrderLines.FirstOrDefault(o => o.BeerId.Equals(@event.BeerId));
		if (orderLine == null)
			return;

		var loadBeerInStock = new LoadBeerInStock(@event.BeerId, new Stock(orderLine.Quantity.Value), new PurchaseOrderId(new Guid(sagaState.PurchaseOrderId)));
		await ServiceBus.SendAsync(loadBeerInStock, cancellationToken);
	}

	public Task HandleAsync(BeerLoadedInStock @event, CancellationToken cancellationToken = new())
	{
		return Task.CompletedTask;
	}
}