using OnlineClothes.Support.Utilities;

namespace OnlineClothes.UnitTest.Support;

[Collection("Support Util")]
public class UtilUrlTest
{
	[Theory]
	[Trait("Category", "Support_Util.Url")]
	[InlineData("xcvz*v-input%%3")]
	[InlineData("fdaszx^%$// a sdf")]
	[InlineData("https://test.com/fdsa a/zz x**F0f^^..-")]
	public void RemoveSpecialChars(string url)
	{
		// arrange
		var whiteList = new[] { '-', '.', '/' };
		var newUrl = Util.Url.RemoveSpecialCharacters(url);

		// act
		var check = newUrl.Any(c => !char.IsLetterOrDigit(c) && !whiteList.Contains(c));

		// assert
		Assert.False(check);
	}
}
