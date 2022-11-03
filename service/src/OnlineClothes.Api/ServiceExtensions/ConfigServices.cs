using System.Reflection;
using OnlineClothes.Application.DependencyInjection;

namespace OnlineClothes.Api.ServiceExtensions;

public static class ConfigServices
{
	public static void Config(this IServiceCollection services, IConfiguration configuration)
	{
		services.InjectApplicationLayer(configuration, Assembly.GetExecutingAssembly());
	}
}
