using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouses.Domain.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Commands;

public sealed class CreateBeerConsumer : CommandConsumerBase<CreateBeer>
{
	protected override ICommandHandlerAsync<CreateBeer> HandlerAsync { get; }
	public CreateBeerConsumer(IServiceProvider serviceProvider, RabbitMQReference rabbitMQReference) : base(serviceProvider, rabbitMQReference)
	{
		var repository = serviceProvider.GetService<IRepository>();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

		HandlerAsync = new CreateBeerCommandHandler(repository!, loggerFactory!);
	}
}