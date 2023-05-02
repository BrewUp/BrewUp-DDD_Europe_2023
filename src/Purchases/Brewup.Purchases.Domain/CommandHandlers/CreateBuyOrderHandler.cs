using Brewup.Purchases.Domain.Entities;
using Brewup.Purchases.Messages.Commands;
using Brewup.Purchases.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace Brewup.Purchases.Domain.CommandHandlers;

public class CreateBuyOrderHandler : CommandHandlerAsync<CreateBuyOrder>
{
	public CreateBuyOrderHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(CreateBuyOrder command, CancellationToken cancellationToken = default)
	{
		var aggregate = Order.Create(new OrderId(command.AggregateId.Value), command.SupplierId, command.Date,
			command.Lines);
		Logger.LogInformation($"Order created aggregateId: {aggregate.Id.Value}");
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}