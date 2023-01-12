using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Persistence.Internal.EntityConfigurations;

public class EntityProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasOne(q => q.ThumbnailImage)
			.WithOne()
			.OnDelete(DeleteBehavior.SetNull);
	}
}
