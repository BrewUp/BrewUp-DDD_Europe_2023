using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Dtos;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace BrewUp.Warehouses.Sagas.Sagas;

public sealed class BeersReceivedSaga : Saga<BeersReceivedSaga.BeersReceivedSagaState>,
	ICommandHandlerAsync<StartBeersReceivedSaga>
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
				// Load Beer in Stock
			}
			else
			{
				// Create Beer
			}
		}
	}
}