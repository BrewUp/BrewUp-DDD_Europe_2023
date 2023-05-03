namespace Brewup.Purchases.ApplicationService.BindingModels;

public class MongoDbSettings
{
	public string ConnectionString { get; set; } = string.Empty;
	public string DatabaseName { get; set; } = string.Empty;
}

public class RabbitMqSettings
{
	public string Host { get; set; } = string.Empty;
	public string ExchangeCommandName { get; set; } = string.Empty;
	public string QueueCommandName { get; set; } = string.Empty;
	public string ExchangeEventName { get; set; } = string.Empty;
	public string QueueEventName { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string ClientId { get; set; } = string.Empty;
}