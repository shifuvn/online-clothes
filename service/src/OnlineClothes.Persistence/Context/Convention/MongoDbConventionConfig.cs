using MongoDB.Bson.Serialization.Conventions;

namespace OnlineClothes.Persistence.Context.Convention;

public static class MongoDbConventionConfig
{
	public static void Configure()
	{
		var camelCaseConventionPack = new ConventionPack
		{
			new CamelCaseElementNameConvention(),
			new IgnoreExtraElementsConvention(true),
			new IgnoreIfNullConvention(true)
		};
		ConventionRegistry.Register("CamelCase", camelCaseConventionPack, _ => true);
	}
}
