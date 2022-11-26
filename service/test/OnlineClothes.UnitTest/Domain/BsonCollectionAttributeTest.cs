using OnlineClothes.Domain.Attributes;

namespace OnlineClothes.UnitTest.Domain;

[Collection("Attribute BsonCollection")]
public class BsonCollectionAttributeTest
{
	[Fact]
	[Trait("Category", "BsonCollectionAttribute")]
	public void GetCollectionName_ForkBsonCollectionWithName()
	{
		// arrange

		// act
		var name = BsonCollectionAttribute.GetName<ForkBsonCollectionWithName>();

		// assert
		Assert.NotNull(name);
		Assert.Equal("Fork", name);
	}

	[Fact]
	[Trait("Category", "BsonCollectionAttribute")]
	public void GetCollectionName_ForkBsonCollectionWithoutName()
	{
		// arrange

		// act
		var name = BsonCollectionAttribute.GetName<ForkBsonCollectionWithoutName>();

		// assert
		Assert.NotNull(name);
		Assert.Equal("ForkBsonCollectionWithoutName", name);
	}

	[BsonCollection("Fork")]
	public class ForkBsonCollectionWithName
	{
	}

	[BsonCollection]
	public class ForkBsonCollectionWithoutName
	{
	}
}
