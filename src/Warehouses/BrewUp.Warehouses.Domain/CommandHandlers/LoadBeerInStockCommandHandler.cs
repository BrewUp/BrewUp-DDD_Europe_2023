﻿using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouses.Domain.Entities;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class LoadBeerInStockCommandHandler : CommandHandlerBaseAsync<LoadBeerInStock>
{
	public LoadBeerInStockCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task ProcessCommand(LoadBeerInStock command, CancellationToken cancellationToken = default)
	{
		var aggregate = await Repository.GetByIdAsync<Beer>(command.BeerId.Value);
		aggregate.LoadBeerInStock(command.BeerId, command.Stock, command.PurchaseOrderId);
		Logger.LogInformation($"Beer loaded in stock aggregateId: {aggregate.Id.Value}");
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}