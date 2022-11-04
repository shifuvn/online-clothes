using Microsoft.Extensions.DependencyInjection;

namespace OnlineClothes.Infrastructure.Context;

public static class UseMongoExtension
{
	public static void UseMongoDb(this IServiceCollection services, Action<MongoDbContextConfiguration> config)
	{
		services.Configure(config);

		services.AddScoped<IMongoDbContext, MongoDbContext>();
	}
}
