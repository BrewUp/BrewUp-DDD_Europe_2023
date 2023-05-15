using BrewUp.Warehouse.SharedKernel.DomainIds;

namespace BrewUp.Warehouse.SharedKernel.Dtos;

public record OrderLine(BeerId BeerId, BeerName BeerName, Quantity Quantity, Price Price);