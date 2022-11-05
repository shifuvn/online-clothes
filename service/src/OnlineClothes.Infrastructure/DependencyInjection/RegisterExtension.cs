using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClothes.Infrastructure.Repositories;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services;
using OnlineClothes.Infrastructure.Services.Abstracts;

namespace OnlineClothes.Infrastructure.DependencyInjection;

public static class RegisterExtension
{
	public static void RegisterRepositories(this IServiceCollection services)
	{
		services.AddTransient<IUserAccountRepository, UserAccountRepository>();
	}

	public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IAuthService, AuthService>();
	}
}
