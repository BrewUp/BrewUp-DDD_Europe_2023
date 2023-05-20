using Brewup.Purchases.Domain.CommandHandlers;
using Brewup.Purchases.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure.RabbitMq.Commands;

public sealed class
	ChangePurchaseOrderStatusToCompleteConsumer : CommandConsumerBase<ChangePurchaseOrderStatusToComplete>
{
	protected override ICommandHandlerAsync<ChangePurchaseOrderStatusToComplete> HandlerAsync { get; }

	public ChangePurchaseOrderStatusToCompleteConsumer(IRepository repository,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference, ILoggerFactory loggerFactory)
		: base(repository, mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlerAsync = new ChangePurchaseOrderStatusToCompleteHandlerAsync(repository, loggerFactory);
	}
}