using MongoDB.Driver;

namespace OnlineClothes.Persistence.Extensions;

public static class MongoAffectedResultExtension
{
	/// <summary>
	/// Check any document is deleted
	/// </summary>
	/// <param name="result"></param>
	/// <returns></returns>
	public static bool Any(this DeleteResult result)
	{
		return result.IsAcknowledged && result.DeletedCount > 0;
	}

	/// <summary>
	/// Check any document is updated
	/// </summary>
	/// <param name="result"></param>
	/// <returns></returns>
	public static bool Any(this UpdateResult result)
	{
		return result.IsAcknowledged && result.IsModifiedCountAvailable && result.ModifiedCount > 0;
	}

	/// <summary>
	/// Check any document is replaced
	/// </summary>
	/// <param name="result"></param>
	/// <returns></returns>
	public static bool Any(this ReplaceOneResult result)
	{
		return result.IsAcknowledged && result.IsModifiedCountAvailable && result.ModifiedCount > 0;
	}
}
