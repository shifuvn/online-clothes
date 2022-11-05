using Microsoft.Extensions.DependencyInjection;

namespace OnlineClothes.MailLib.Service;

public static class RegisterExtension
{
	public static void UseRazorRenderer(this IServiceCollection services)
	{
		services.AddTransient<IRazorRenderer, RazorRenderer>();
	}
}
