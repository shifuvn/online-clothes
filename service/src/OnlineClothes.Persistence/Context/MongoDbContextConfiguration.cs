namespace OnlineClothes.Persistence.Context;

public class MongoDbContextConfiguration
{
	public string ConnectionString { get; set; } = null!;
	public string Database { get; set; } = null!;
}
