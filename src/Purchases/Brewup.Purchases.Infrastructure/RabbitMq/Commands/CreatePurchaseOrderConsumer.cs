using Brewup.Purchases.Domain.CommandHandlers;
using Brewup.Purchases.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure.RabbitMq.Commands;

public sealed class CreatePurchaseOrderConsumer : CommandConsumerBase<CreatePurchaseOrder>
{
	protected override ICommandHandlerAsync<CreatePurchaseOrder> HandlerAsync { get; }


	//public CreatePurchaseOrderConsumer(IRepository repository, RabbitMQReference rabbitMQReference, IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory) 
	//	: base(repository, rabbitMQReference, mufloneConnectionFactory, loggerFactory)
	//{
	//	HandlerAsync = new CreatePurchaseOrderHandlerAsync(repository, loggerFactory);
	//}
	public CreatePurchaseOrderConsumer(IServiceProvider serviceProvider, RabbitMQReference rabbitMQReference) : base(
		serviceProvider, rabbitMQReference)
	{
		var repository = serviceProvider.GetService<IRepository>();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
		HandlerAsync = new CreatePurchaseOrderHandlerAsync(repository!, loggerFactory!);
	}
}