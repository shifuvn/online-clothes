using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClothes.Support.Entity;

public abstract class EntityBase : EntityBase<int>
{
}

public abstract class EntityBase<TKey> : EntityDatetimeBase, IEntity<TKey>
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Required]
	public TKey Id { get; set; } = default!;
}

public interface IEntity<TKey> : IEntityDatetimeSupport
{
	TKey Id { get; set; }
}

public interface IEntity : IEntity<int>
{
}
