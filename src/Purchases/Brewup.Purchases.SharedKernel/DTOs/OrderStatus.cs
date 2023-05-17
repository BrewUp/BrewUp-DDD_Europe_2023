namespace Brewup.Purchases.SharedKernel.DTOs;

public class OrderStatus: Enumeration
{
	public static OrderStatus Created = new OrderStatus(1, nameof(Created).ToLowerInvariant());
	public static OrderStatus Sent = new OrderStatus(1, nameof(Sent).ToLowerInvariant());
	public static OrderStatus Complete = new OrderStatus(2, nameof(Complete).ToLowerInvariant());
	public static OrderStatus Cancelled = new OrderStatus(3, nameof(Cancelled).ToLowerInvariant());

	public static IEnumerable<OrderStatus> List() => new[] { Sent, Complete, Cancelled};

	public OrderStatus(int id, string name)
		: base(id, name)
	{
	}

	public static OrderStatus FromName(string name)
	{
		var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

		if (state == null)
			throw new Exception($"Possible values for SeatState: {string.Join(",", List().Select(s => s.Name))}");

		return state;
	}

	public static OrderStatus From(int id)
	{
		var state = List().SingleOrDefault(s => s.Id == id);

		if (state == null)
			throw new Exception($"Possible values for SeatState: {string.Join(",", List().Select(s => s.Name))}");

		return state;
	}


}