using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClothes.Support.Entity;

public abstract class EntityBase : EntityDatetimeBase, IEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Required]
	public int Id { get; } = default;
}

public interface IEntity<out TKey> : IEntityDatetimeSupport
{
	TKey Id { get; }
}

public interface IEntity : IEntity<int>
{
}
