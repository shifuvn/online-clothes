using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClothes.Persistence.MySql.Context;

namespace OnlineClothes.Persistence.MySql;

public static class DependencyInjection
{
	public static IServiceCollection RegisterPersistenceLayer(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(options =>
		{
			var connectionString = configuration.GetConnectionString("AppContext");
			options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31)),
				mySqlOption =>
				{
					mySqlOption.MigrationsAssembly("OnlineClothes.Persistence");
					mySqlOption.EnableRetryOnFailure();
				});
		});

		return services;
	}
}
