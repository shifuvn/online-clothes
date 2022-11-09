using OnlineClothes.Support.Utilities;

namespace OnlineClothes.UnitTest.Support;

[Collection("Support Util")]
public class UtilListTest
{
	[Fact]
	[Trait("Category", "Support_Util.Array")]
	public void EmptyList_ReturnEmpty()
	{
		var emptyList = Util.Array.Empty<string>();

		Assert.NotNull(emptyList);
		Assert.Empty(emptyList);
	}

	[Theory]
	[Trait("Category", "Support_Util.Array")]
	[MemberData(nameof(ProvidedTestData))]
	public void Empty_ReturnTrue(IEnumerable<int>? list)
	{
		// arrange
		var check = Util.Array.IsNullOrEmpty(list);

		// act

		// assert
		Assert.True(check);
	}

	[Fact]
	[Trait("Category", "Support_Util.Array")]
	public void Null_ReturnTrue()
	{
		// arrange
		IEnumerable<int>? enumerable = null;

		// act
		var check = Util.Array.IsNullOrEmpty(enumerable);

		// assert
		Assert.True(check);
	}

	public static IEnumerable<object[]?> ProvidedTestData()
	{
		yield return new object[] { new List<int>() };
		yield return new object[]
		{
			ArraySegment<int>.Empty
		};

		yield return new object[]
		{
			Array.Empty<int>()
		};

		yield return new object[] { new int[] { } };
	}
}
