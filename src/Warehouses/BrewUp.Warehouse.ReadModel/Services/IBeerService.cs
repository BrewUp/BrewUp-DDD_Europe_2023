﻿using BrewUp.Warehouse.SharedKernel.DomainIds;
using BrewUp.Warehouse.SharedKernel.Dtos;

namespace BrewUp.Warehouse.ReadModel.Services;

public interface IBeerService
{
	Task<BeerId> AddBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken = default);
	Task LoadBeerInStockAsync(BeerId beerId, Stock stock, Price price, CancellationToken cancellationToken = default);
}