using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Persistence.Internal.EntityConfigurations;

public class EntityProductTypeConfigure : IEntityTypeConfiguration<ProductType>
{
	public void Configure(EntityTypeBuilder<ProductType> builder)
	{
		builder.HasIndex(q => q.Name)
			.IsUnique();
	}
}
