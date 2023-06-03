﻿namespace Brewup.Purchases.SharedKernel.DTOs;

public class Status: Enumeration
{
	public static Status Created = new Status(1, nameof(Created).ToLowerInvariant());
	public static Status Sent = new Status(1, nameof(Sent).ToLowerInvariant());
	public static Status Complete = new Status(2, nameof(Complete).ToLowerInvariant());
	public static Status Cancelled = new Status(3, nameof(Cancelled).ToLowerInvariant());

	public static IEnumerable<Status> List() => new[] { Sent, Complete, Cancelled};

	public Status(int id, string name)
		: base(id, name)
	{
	}

	public static Status FromName(string name)
	{
		var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

		if (state == null)
			throw new Exception($"Possible values for SeatState: {string.Join(",", List().Select(s => s.Name))}");

		return state;
	}

	public static Status From(int id)
	{
		var state = List().SingleOrDefault(s => s.Id == id);

		if (state == null)
			throw new Exception($"Possible values for SeatState: {string.Join(",", List().Select(s => s.Name))}");

		return state;
	}


}