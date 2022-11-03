using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Domain.Entities;

public abstract class MongoEntityBase : IEntity<string>
{
	private string? _id;

	[BsonId]
	public string Id
	{
		get
		{
			if (!string.IsNullOrEmpty(_id))
			{
				return _id;
			}

			_id = ObjectId.GenerateNewId().ToString();
			return _id;
		}
	}

	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }
}