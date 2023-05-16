using BrewUp.Warehouse.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Events;

public sealed class BeerCreatedConsumer : DomainEventsConsumerBase<BeerCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerCreated>> HandlersAsync { get; }

	public BeerCreatedConsumer(IServiceProvider serviceProvider,
		RabbitMQReference rabbitMQReference) : base(serviceProvider, rabbitMQReference)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<BeerCreated>>();
	}
}