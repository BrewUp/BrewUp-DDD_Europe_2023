using Brewup.Purchases.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure.RabbitMq.Events;

public sealed class PurchaseOrderStatusChangedToCompleteConsumer : DomainEventsConsumerBase<PurchaseOrderStatusChangedToComplete>
{
	protected override IEnumerable<IDomainEventHandlerAsync<PurchaseOrderStatusChangedToComplete>> HandlersAsync { get; }

	public PurchaseOrderStatusChangedToCompleteConsumer(IServiceProvider serviceProvider, IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference,
		ILoggerFactory loggerFactory) : base(mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<PurchaseOrderStatusChangedToComplete>>();
	}
}