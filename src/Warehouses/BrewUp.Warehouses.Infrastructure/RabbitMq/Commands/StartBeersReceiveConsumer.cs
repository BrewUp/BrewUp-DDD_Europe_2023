﻿using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Models;
using Muflone.Transport.RabbitMQ.Saga.Consumers;

namespace BrewUp.Warehouses.Infrastructure.RabbitMq.Commands;

public sealed class StartBeersReceivedSagaConsumer : SagaStartedByConsumerBase<StartBeersReceivedSaga>
{
	public StartBeersReceivedSagaConsumer(IServiceBus serviceBus,
		ISagaRepository sagaRepository,
		IRepository repository,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference,
		ILoggerFactory loggerFactory) : base(repository, mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlerAsync = new BeersReceivedSaga(serviceBus, sagaRepository, loggerFactory);
	}

	protected override ISagaStartedByAsync<StartBeersReceivedSaga> HandlerAsync { get; }
}