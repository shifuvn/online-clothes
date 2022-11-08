using OnlineClothes.Support.Utilities;

namespace OnlineClothes.UnitTest.Support;

[Collection("Support Util")]
public class UtilListTest
{
	[Fact]
	[Trait("Category", "Support_Util.List")]
	public void EmptyList_ReturnEmpty()
	{
		var emptyList = Util.List.Empty<string>();

		Assert.NotNull(emptyList);
		Assert.Empty(emptyList);
	}

	[Theory]
	[Trait("Category", "Support_Util.List")]
	[MemberData(nameof(ProvidedTestData))]
	public void Empty_ReturnTrue(IEnumerable<int>? list)
	{
		// arrange
		var check = Util.List.IsNullOrEmpty(list);

		// act

		// assert
		Assert.True(check);
	}

	[Fact]
	[Trait("Category", "Support_Util.List")]
	public void Null_ReturnTrue()
	{
		// arrange
		IEnumerable<int>? enumerable = null;

		// act
		var check = Util.List.IsNullOrEmpty(enumerable);

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
