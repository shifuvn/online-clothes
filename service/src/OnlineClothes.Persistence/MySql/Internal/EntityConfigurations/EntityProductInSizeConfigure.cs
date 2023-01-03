using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Persistence.MySql.Internal.EntityConfigurations;

public class EntityProductInSizeConfigure : IEntityTypeConfiguration<ProductInSize>
{
	public void Configure(EntityTypeBuilder<ProductInSize> builder)
	{
		builder.HasKey(q => new { q.ClotheDetailId, q.Size });
	}
}
