using OnlineClothes.Application.Commons;

namespace OnlineClothes.UnitTest.Regex;

[Collection("Regex Test")]
public class RegexTest
{
	[Theory]
	[Trait("Category", "Regex")]
	[InlineData("HATTEST01")]
	[InlineData("TEST-01")]
	[InlineData("asz09Z-1")]
	public void RegexSku_Valid(string sku)
	{
		// arr
		var regex = new System.Text.RegularExpressions.Regex(RegexPattern.ValidSku);

		// act

		// assert
		Assert.Matches(regex, sku);
	}
}
