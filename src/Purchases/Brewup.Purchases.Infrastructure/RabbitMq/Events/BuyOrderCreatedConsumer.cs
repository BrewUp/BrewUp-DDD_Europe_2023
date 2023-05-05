using Brewup.Purchases.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure.RabbitMq.Events;

public sealed class BuyOrderCreatedConsumer : DomainEventsConsumerBase<BuyOrderCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BuyOrderCreated>> HandlersAsync { get; }

	public BuyOrderCreatedConsumer(IServiceProvider serviceProvider,
		RabbitMQReference rabbitMQReference,
		IMufloneConnectionFactory mufloneConnectionFactory,
		ILoggerFactory loggerFactory) : base(rabbitMQReference, mufloneConnectionFactory, loggerFactory)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<BuyOrderCreated>>();
	}
}