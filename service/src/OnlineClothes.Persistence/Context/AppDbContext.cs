using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineClothes.BuildIn.Entity;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Persistence.Context;

public class AppDbContext : DbContext
{
	// TODO: logger executing time
	private static readonly ILoggerFactory LoggerFactInstance = LoggerFactory.Create(
		builder =>
		{
			builder
				.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
				.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Warning)
				.AddConsole();
		});


	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
		{
			optionsBuilder.UseLoggerFactory(LoggerFactInstance)
				.EnableSensitiveDataLogging();
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
		CancellationToken cancellationToken = new())
	{
		TrackEntitySupportDateTime();
		return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}


	/// <summary>
	/// Solve entity dataTime tracker
	/// </summary>
	private void TrackEntitySupportDateTime()
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
	}

	#region Dbset

	public DbSet<AccountUser> AccountUsers { get; set; } = null!;
	public DbSet<AccountTokenCode> AccountTokens { get; set; } = null!;
	public DbSet<AccountCart> AccountCarts { get; set; } = null!;
	public DbSet<CartItem> CartItems { get; set; } = null!;
	public DbSet<Brand> Brands { get; set; } = null!;
	public DbSet<Category> Categories { get; set; } = null!;
	public DbSet<Product> Products { get; set; } = null!;
	public DbSet<ProductSku> ProductSkus { get; set; } = null!;
	public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
	public DbSet<Order> Orders { get; set; } = null!;
	public DbSet<OrderItem> OrderItems { get; set; } = null!;
	public DbSet<ImageObject> Images { get; set; } = null!;

	#endregion
}
