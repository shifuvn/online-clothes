using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Persistence.MySql.Internal.EntityConfigurations;

public class EntityClotheMaterialConfigure : IEntityTypeConfiguration<ProductInMaterial>
{
	public void Configure(EntityTypeBuilder<ProductInMaterial> builder)
	{
		builder.HasKey(q => new { q.ClotheDetailId, q.MaterialId });
	}
}
