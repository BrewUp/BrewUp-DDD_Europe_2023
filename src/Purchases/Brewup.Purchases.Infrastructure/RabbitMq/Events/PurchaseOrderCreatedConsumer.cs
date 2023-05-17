using Brewup.Purchases.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure.RabbitMq.Events;

public sealed class PurchaseOrderCreatedConsumer : DomainEventsConsumerBase<PurchaseOrderCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<PurchaseOrderCreated>> HandlersAsync { get; }

	//public PurchaseOrderCreatedConsumer(IServiceProvider serviceProvider, RabbitMQReference rabbitMQReference, IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory) 
	//	: base(rabbitMQReference, mufloneConnectionFactory, loggerFactory)
	//{
	//	HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<PurchaseOrderCreated>>();
	//}

	public PurchaseOrderCreatedConsumer(IServiceProvider serviceProvider, RabbitMQReference rabbitMQReference) : base(
		serviceProvider, rabbitMQReference)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<PurchaseOrderCreated>>();
	}
}