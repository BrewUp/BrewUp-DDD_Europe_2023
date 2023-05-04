using Brewup.Purchases.Domain.CommandHandlers;
using Brewup.Purchases.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure.RabbitMq.Commands;

public sealed class CreateBuyOrderConsumer : CommandConsumerBase<CreateBuyOrder>
{
	protected override ICommandHandlerAsync<CreateBuyOrder> HandlerAsync { get; }

	public CreateBuyOrderConsumer(IRepository repository,
		RabbitMQReference rabbitMQReference,
		IMufloneConnectionFactory mufloneConnectionFactory,
		ILoggerFactory loggerFactory) : base(repository, rabbitMQReference, mufloneConnectionFactory, loggerFactory)
	{
		HandlerAsync = new CreateBuyOrderHandlerAsync(repository, loggerFactory);
	}
}