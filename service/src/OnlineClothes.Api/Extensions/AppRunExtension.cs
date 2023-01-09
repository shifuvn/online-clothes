using Microsoft.EntityFrameworkCore;
using OnlineClothes.Persistence.Context;

namespace OnlineClothes.Api.Extensions;

public static class AppRunExtension
{
	public static void MigrateDatabase(this IHost app)
	{
		using var scope = app.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		dbContext.Database.Migrate();
	}
}
