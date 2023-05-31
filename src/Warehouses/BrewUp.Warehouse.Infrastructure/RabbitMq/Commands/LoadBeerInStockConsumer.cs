using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouses.Domain.CommandHandlers;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Commands;

public sealed class LoadBeerInStockConsumer : CommandConsumerBase<LoadBeerInStock>
{
	protected override ICommandHandlerAsync<LoadBeerInStock> HandlerAsync { get; }

	public LoadBeerInStockConsumer(IRepository repository,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference,
		ILoggerFactory loggerFactory) : base(repository, mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlerAsync = new LoadBeerInStockCommandHandler(repository, loggerFactory);
	}
}