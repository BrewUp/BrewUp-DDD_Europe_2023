﻿using Muflone.Core;

namespace Brewup.Purchases.Domain.Entities;

public class Price : ValueObject
{
	public double Value { get; }
	public string Currency { get; }

	public Price(double value, string currency)
	{
		Value = value;
		Currency = currency;
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
		yield return Currency;
	}
}