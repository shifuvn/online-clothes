using System.Reflection;
using OnlineClothes.Support.Exceptions;

namespace OnlineClothes.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class BsonCollectionAttribute : Attribute
{
	public BsonCollectionAttribute(string? name = null)
	{
		Name = name;
	}

	public string? Name { get; }

	public static string GetName<T>()
	{
		var collection = typeof(T).GetCustomAttribute<BsonCollectionAttribute>();
		NullValueReferenceException.ThrowIfNull(collection);

		return string.IsNullOrEmpty(collection.Name) ? typeof(T).Name : collection.Name;
	}
}
