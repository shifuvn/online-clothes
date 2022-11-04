using System.Reflection;

namespace OnlineClothes.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class BsonCollectionAttribute : Attribute
{
	public BsonCollectionAttribute(string name)
	{
		Name = name;
	}

	public string Name { get; }

	public static string GetName<T>() where T : class
	{
		var collectionName = typeof(T).GetCustomAttribute<BsonCollectionAttribute>();
		return collectionName is null ? typeof(T).Name : collectionName.Name;
	}
}
