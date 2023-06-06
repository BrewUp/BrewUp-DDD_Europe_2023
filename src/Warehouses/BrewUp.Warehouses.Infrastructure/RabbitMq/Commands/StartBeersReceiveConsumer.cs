using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.Sagas.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructure.RabbitMq.Commands;

public sealed class StartBeersReceiveConsumer : CommandConsumerBase<StartBeersReceivedSaga>
{
	protected override ICommandHandlerAsync<StartBeersReceivedSaga> HandlerAsync { get; }

	public StartBeersReceiveConsumer(IServiceBus serviceBus,
		ISagaRepository sagaRepository,
		IBeerService beerService,
		IRepository repository,
		IMufloneConnectionFactory mufloneConnectionFactory,
		ILoggerFactory loggerFactory) : base(repository, mufloneConnectionFactory, loggerFactory)
	{
		HandlerAsync = new BeersReceivedSaga(serviceBus, sagaRepository, beerService, loggerFactory);
	}
}