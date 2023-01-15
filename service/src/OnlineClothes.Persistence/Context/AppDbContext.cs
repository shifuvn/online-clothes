using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using OnlineClothes.BuildIn.Entity;
using OnlineClothes.BuildIn.Entity.Event;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Persistence.Internal.Extensions;

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

	public IList<object> DomainEvents { get; } = new List<object>();

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

	public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
		CancellationToken cancellationToken = new())
	{
		TrackEntitySupportDateTime();
		TrackEntityDomainEvents();

		return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}

	/// <summary>
	/// Solve entity domain events
	/// </summary>
	private void TrackEntityDomainEvents()
	{
		foreach (var entityEntry in ChangeTracker.Entries<ISupportDomainEvent>())
		{
			var eventPayloads = entityEntry.Entity.EventPayloads;
			AppendDomainEventFromPayload(eventPayloads, entityEntry);
		}
	}

	/// <summary>
	/// Add entity payload to domain event
	/// </summary>
	/// <param name="eventPayloads"></param>
	/// <param name="entityEntry"></param>
	private void AppendDomainEventFromPayload(
		IEnumerable<DomainEventPayload> eventPayloads,
		EntityEntry<ISupportDomainEvent> entityEntry)
	{
		foreach (var domainEventPayload in eventPayloads)
		{
			var openType = typeof(DomainEvent<>);
			Type[] tArgs = { entityEntry.Entity.GetType() };
			var target = openType.MakeGenericType(tArgs);

			var domainEvent = Activator.CreateInstance(target,
				domainEventPayload.Key,
				entityEntry.State.GetDomainEventAction(),
				domainEventPayload.Value);
			DomainEvents.Add(domainEvent!);
		}
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
