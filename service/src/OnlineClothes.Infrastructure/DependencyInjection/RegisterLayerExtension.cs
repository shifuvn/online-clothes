using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClothes.Infrastructure.StandaloneConfigurations;

namespace OnlineClothes.Infrastructure.DependencyInjection;

public static class RegisterLayerExtension
{
	public static void RegisterInfrastructureLayer(this IServiceCollection services,
		IConfiguration configuration,
		Assembly? assembly = null)
	{
		services.RegisterRepositories();
		services.RegisterServices(configuration);

		services.Configure<AppDomainConfiguration>(configuration.GetSection("AppDomain"));
	}
}
