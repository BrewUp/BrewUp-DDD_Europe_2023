using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouses.Domain.CommandHandlers;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Commands;

public sealed class CreateBeerConsumer : CommandConsumerBase<CreateBeer>
{
	protected override ICommandHandlerAsync<CreateBeer> HandlerAsync { get; }
	public CreateBeerConsumer(IRepository repository,
		RabbitMQReference rabbitMQReference,
		IMufloneConnectionFactory mufloneConnectionFactory,
		ILoggerFactory loggerFactory) : base(repository, rabbitMQReference, mufloneConnectionFactory, loggerFactory)
	{
		HandlerAsync = new CreateBeerCommandHandler(repository, loggerFactory);
	}
}