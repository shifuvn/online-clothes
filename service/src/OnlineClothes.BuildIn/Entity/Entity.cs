using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineClothes.BuildIn.Entity.Event;

namespace OnlineClothes.BuildIn.Entity;

public abstract class EntityBase : EntityBase<int>
{
}

public abstract class EntityBase<TKey> : SupportDomainEvent, IEntity<TKey>
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Required]
	public TKey Id { get; set; } = default!;

	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }
}

public interface IEntity<TKey>
{
}

public interface IEntity : IEntity<int>
{
}
