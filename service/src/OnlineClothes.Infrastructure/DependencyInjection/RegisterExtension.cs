using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClothes.Infrastructure.Repositories;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Auth;
using OnlineClothes.Infrastructure.Services.Auth.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing;
using OnlineClothes.Infrastructure.Services.Mailing.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing.Engine;
using OnlineClothes.Infrastructure.Services.UserContext;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;

namespace OnlineClothes.Infrastructure.DependencyInjection;

public static class RegisterExtension
{
	public static void RegisterRepositories(this IServiceCollection services)
	{
		services.AddTransient<IUserAccountRepository, UserAccountRepository>();
		services.AddTransient<IAccountTokenCodeRepository, AccountTokenCodeRepository>();
	}

	public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		// auth
		services.AddTransient<IAuthService, AuthService>();

		// mailing
		services.AddSingleton<IMailingProviderConnection, MailingProviderConnection>();
		services.AddTransient<IMailingService, MailingService>();
		services.Configure<MailingProviderConfiguration>(configuration.GetSection("Mailing"));
		services.AddTransient<RazorEngineRenderer>();

		// context
		services.AddTransient<IUserContext, UserContext>();
	}
}
