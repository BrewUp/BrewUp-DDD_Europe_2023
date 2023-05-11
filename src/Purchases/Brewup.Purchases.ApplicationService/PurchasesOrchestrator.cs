﻿using Brewup.Purchases.ApplicationService.BindingModels;
using Brewup.Purchases.Messages.Commands;
using Brewup.Purchases.SharedKernel.DomainIds;
using Muflone.Persistence;

namespace Brewup.Purchases.ApplicationService;

public sealed class PurchasesOrchestrator : IPurchasesOrchestrator
{
    private readonly IServiceBus _serviceBus;

    public PurchasesOrchestrator(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public async Task<string> CreateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        //Application level validation
        var createOrder = new CreateBuyOrder(new BuyOrderId(order.Id),
            new SupplierId(order.SupplierId), order.Date,
            order.Lines.ToDto());

        await _serviceBus.SendAsync(createOrder, cancellationToken);

        return order.Id.ToString();
    }
}