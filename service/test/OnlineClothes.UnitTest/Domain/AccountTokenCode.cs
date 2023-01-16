using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.UnitTest.Domain;

[Collection("Domain AccountTokenCode")]
public class AccountTokenCodeTest
{
	[Fact]
	[Trait("Category", "Domain_AccountTokenCode")]
	public void CheckValidCode_ReturnTrue()
	{
		// arr
		var code = new AccountTokenCode("test@mail.com", AccountTokenType.Verification, TimeSpan.FromSeconds(10));

		// act
		var result = code.IsValid();

		// assert
		Assert.True(result);
	}

	[Fact]
	[Trait("Category", "Domain_AccountTokenCode")]
	public void CheckValidCode_Expired_ReturnFalse()
	{
		// arr
		var code = new AccountTokenCode("test@mail.com", AccountTokenType.Verification, TimeSpan.FromSeconds(1));
		Thread.Sleep(TimeSpan.FromSeconds(2));

		// act
		var result = code.IsValid();

		// assert
		Assert.False(result);
	}
}
