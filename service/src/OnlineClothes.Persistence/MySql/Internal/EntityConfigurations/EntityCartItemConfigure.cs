using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Persistence.MySql.Internal.EntityConfigurations;

internal class EntityCartItemConfigure : IEntityTypeConfiguration<CartItem>
{
	public void Configure(EntityTypeBuilder<CartItem> builder)
	{
		builder.HasKey(q => new { q.CartId, q.ProductId });
	}
}
