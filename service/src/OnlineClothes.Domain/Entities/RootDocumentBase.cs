using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Domain.Entities;

public abstract class RootDocumentBase : IEntity<string>, IDateTimeSupportEntity
{
	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }

	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
}
