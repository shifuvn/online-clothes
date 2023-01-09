using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Middlewares;
using OnlineClothes.Application.PipelineBehaviors;

namespace OnlineClothes.Application;

public static class DependencyInjection
{
	public static void RegisterApplicationLayer(this IServiceCollection services,
		IConfiguration configuration,
		Assembly? assembly = null)
	{
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddTransient<ExceptionHandlingMiddleware>();

		services.AddTransient<AccountActivationHelper>();
	}
}

public static class ApplicationLayerAssembly
{
	public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
}
