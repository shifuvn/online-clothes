using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Persistence.Internal.EntityConfigurations;

public class EntityProductSkuConfigure : IEntityTypeConfiguration<ProductSku>
{
	public void Configure(EntityTypeBuilder<ProductSku> builder)
	{
		builder.HasOne(q => q.Image)
			.WithOne()
			.OnDelete(DeleteBehavior.SetNull);
	}
}
