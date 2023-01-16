using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Uow;

namespace OnlineClothes.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection RegisterPersistenceLayer(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(contextOptionsBuilder =>
		{
			var connectionString = configuration.GetConnectionString("PgBouncer");
			contextOptionsBuilder.UseNpgsql(connectionString,
				npgsqlOptionsBuilder =>
				{
					// TODO: remove hard-code
					npgsqlOptionsBuilder.MigrationsAssembly("OnlineClothes.Persistence");
				});
			contextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}

public static class PersistenceAssembly
{
	public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
}
