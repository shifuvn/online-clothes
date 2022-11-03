using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Domain.Entities;

public abstract class MongoEntityBase : IEntity<string>, IDateTimeSupportEntity
{
	private string? _id;

	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }

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
}
