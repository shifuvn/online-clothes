using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.MySql.Context;

public class AppDbContext : DbContext
{
	private static readonly ILoggerFactory LoggerFactInstance = LoggerFactory.Create(
		builder =>
		{
			builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
				.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Warning)
				.AddConsole();
		});

	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.UseLoggerFactory(LoggerFactInstance)
			.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
		CancellationToken cancellationToken = new())
	{
		foreach (var entityEntry in ChangeTracker.Entries<IEntityDatetimeSupport>())
		{
			switch (entityEntry.State)
			{
				case EntityState.Added:
					entityEntry.Entity.CreatedAt = DateTime.UtcNow;
					break;
				case EntityState.Deleted:
				case EntityState.Modified:
					entityEntry.Entity.ModifiedAt = DateTime.UtcNow;
					break;
			}
		}


		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}
}
