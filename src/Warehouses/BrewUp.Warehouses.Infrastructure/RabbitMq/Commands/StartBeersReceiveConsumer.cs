using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.Sagas.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouses.Infrastructure.RabbitMq.Commands;

public sealed class StartBeersReceiveConsumer : CommandConsumerBase<StartBeersReceivedSaga>
{
	protected override ICommandHandlerAsync<StartBeersReceivedSaga> HandlerAsync { get; }

	public StartBeersReceiveConsumer(IServiceBus serviceBus,
		ISagaRepository sagaRepository,
		IRepository repository,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference,
		ILoggerFactory loggerFactory) : base(repository, mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlerAsync = new BeersReceivedSaga(serviceBus, sagaRepository, loggerFactory);
	}
}