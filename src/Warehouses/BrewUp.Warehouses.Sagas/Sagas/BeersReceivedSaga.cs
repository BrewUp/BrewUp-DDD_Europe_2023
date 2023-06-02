using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.Messages.Events;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.DomainIds;
using BrewUp.Warehouses.SharedKernel.Dtos;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace BrewUp.Warehouses.Sagas.Sagas;

public sealed class BeersReceivedSaga : Saga<BeersReceivedSaga.BeersReceivedSagaState>,
	ICommandHandlerAsync<StartBeersReceivedSaga>,
	IDomainEventHandlerAsync<BeerCreated>,
	IDomainEventHandlerAsync<BeerLoadedInStock>
{
	private readonly IBeerService _beerService;

	public class BeersReceivedSagaState
	{
		public string PurchaseOrderId { get; set; } = string.Empty;

		public IEnumerable<OrderLine> OrderLines { get; set; } = Enumerable.Empty<OrderLine>();

		public DateTime StartedAt { get; set; } = DateTime.MinValue;
		public DateTime FinishedAt { get; set; } = DateTime.MinValue;
	}

	public BeersReceivedSaga(IServiceBus serviceBus, ISagaRepository repository, IBeerService beerService, ILoggerFactory loggerFactory)
		: base(serviceBus, repository, loggerFactory)
	{
		_beerService = beerService;
	}

	public async Task HandleAsync(StartBeersReceivedSaga command, CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();

		SagaState = new BeersReceivedSagaState
		{
			PurchaseOrderId = command.PurchaseOrderId.Value.ToString(),
			OrderLines = command.OrderLines,
			StartedAt = DateTime.UtcNow
		};
		await Repository.SaveAsync(command.MessageId, SagaState);

		foreach (var orderLine in command.OrderLines)
		{
			var beer = await _beerService.GetBeerAsync(orderLine.BeerId, cancellationToken);
			if (beer != null)
			{
				//TODO: Update beer movements, with prices and quantities. We will simply updates stock and prices not movements.

			}
			else
			{
				var createBeer = new CreateBeer(orderLine.BeerId, command.MessageId, orderLine.BeerName);
				await ServiceBus.SendAsync(createBeer, cancellationToken);
			}
		}
	}

	public async Task HandleAsync(BeerCreated @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		if (correlationId.Equals(Guid.Empty))
			return;

		var sagaState = await Repository.GetByIdAsync<BeersReceivedSagaState>(correlationId);
		var orderLine = sagaState.OrderLines.FirstOrDefault(o => o.BeerId.Equals(@event.BeerId));
		if (orderLine == null)
			return;

		var loadBeerInStock = new LoadBeerInStock(@event.BeerId, correlationId, new Stock(orderLine.Quantity.Value),
			new Price(orderLine.Price.Value, orderLine.Price.Currency),
			new PurchaseOrderId(new Guid(sagaState.PurchaseOrderId)));
		await ServiceBus.SendAsync(loadBeerInStock, cancellationToken);
	}

	public async Task HandleAsync(BeerLoadedInStock @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		if (correlationId.Equals(Guid.Empty))
			return;

		var sagaState = await Repository.GetByIdAsync<BeersReceivedSagaState>(correlationId);
		sagaState.FinishedAt = DateTime.UtcNow;
		await Repository.SaveAsync(correlationId, sagaState);
	}
}