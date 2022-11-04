using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using OnlineClothes.Domain.Attributes;
using OnlineClothes.Infrastructure.Context.Convention;

namespace OnlineClothes.Infrastructure.Context;

public class MongoDbContext : IMongoDbContext
{
	private readonly MongoDbContextConfiguration _configuration;
	private readonly ILogger<MongoDbContext> _logger;

	public MongoDbContext(ILogger<MongoDbContext> logger, IOptions<MongoDbContextConfiguration> configurationOptions)
	{
		_logger = logger;
		_configuration = configurationOptions.Value;
	}

	private IMongoDatabase Database { get; set; } = null!;
	private MongoClient? Client { get; set; }

	public IMongoDatabase GetDatabase()
	{
		return Database;
	}

	public IMongoCollection<TCollection> GetCollection<TCollection>(string? name = null) where TCollection : class
	{
		Configure();
		var collectionName = BsonCollectionAttribute.GetName<TCollection>();
		var collection = Database.GetCollection<TCollection>(collectionName);
		return collection;
	}

	private void Configure()
	{
		if (Client != null)
		{
			return;
		}

		_logger.LogInformation("Init Mongo session");

		MongoDbConventionConfig.Configure();

		var mongoClientSetting = ConfigureMongoClientSettings();
		Client = new MongoClient(mongoClientSetting);
		Database = Client.GetDatabase(_configuration.DatabaseName);
	}

	private MongoClientSettings ConfigureMongoClientSettings()
	{
		var mongoUrl = new MongoUrl(_configuration.ConnectionString);
		var mongoClientSetting = MongoClientSettings.FromUrl(mongoUrl);
		mongoClientSetting.MaxConnectionIdleTime = TimeSpan.FromMinutes(5);
		mongoClientSetting.MaxConnectionLifeTime = TimeSpan.FromMinutes(10);

		// log query
		if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
		{
			mongoClientSetting.ClusterConfigurator = cb =>
			{
				cb.Subscribe<CommandStartedEvent>(e =>
					_logger.LogInformation("{0}", e.Command.ToString()));
			};
		}

		return mongoClientSetting;
	}
}
