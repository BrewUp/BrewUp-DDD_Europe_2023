using BrewUp.Warehouse.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Events;

public sealed class BeersReceivedConsumer : IntegrationEventsConsumerBase<BeersReceived>
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<BeersReceived>> HandlersAsync { get; }

	public BeersReceivedConsumer(IServiceProvider serviceProvider,
		RabbitMQReference rabbitMQReference) : base(serviceProvider, rabbitMQReference)
	{
		HandlersAsync = serviceProvider.GetServices<IIntegrationEventHandlerAsync<BeersReceived>>();
	}
}