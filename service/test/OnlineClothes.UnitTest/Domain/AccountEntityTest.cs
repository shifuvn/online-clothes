using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.UnitTest.Domain;

[Collection("Entities")]
public class AccountEntityTest : IClassFixture<AccountEntityTestFixture>
{
	private readonly AccountEntityTestFixture fixture;

	public AccountEntityTest(AccountEntityTestFixture fixture)
	{
		this.fixture = fixture;
	}

	[Fact]
	[Trait("Category", "Account")]
	public void Account_CreateNew()
	{
		// arrange

		// act
		var newAccount = fixture.TestClientAccount;

		// assert
		Assert.Equal(fixture.Email.ToUpper(), newAccount.NormalizeEmail);
		Assert.Equal("Client", newAccount.Role);
		Assert.NotEqual(fixture.Password, newAccount.NormalizeEmail);
		Assert.False(newAccount.IsActivated);
	}

	[Fact]
	[Trait("Category", "Account")]
	public void Account_Activate()
	{
		// arrange
		var newAccount = fixture.TestClientAccount;

		// act
		newAccount.Activate();

		// assert
		Assert.True(newAccount.IsActivated);
	}

	[Fact]
	[Trait("Category", "Account")]
	public void Account_GivenClientAccount_HasClientRole()
	{
		// arrange
		var newAccount = fixture.TestClientAccount;

		// act
		var hasRole = newAccount.HasRole(UserAccountRole.Client);

		// assert
		Assert.True(hasRole);
	}

	[Fact]
	[Trait("Category", "Account")]
	public void Account_GivenUser_ShouldVerifyPasswordTrue()
	{
		// arrange
		var newAccount = fixture.TestClientAccount;

		// act
		var verify = newAccount.VerifyPassword(fixture.Password);

		// assert
		Assert.True(verify);
	}

	[Fact]
	[Trait("Category", "Account")]
	public void Account_GivenUser_ShouldVerifyPasswordFalse()
	{
		// arrange
		var newAccount = fixture.TestClientAccount;

		// act
		var verify = newAccount.VerifyPassword(fixture.Password + "1@");

		// assert
		Assert.False(verify);
	}
}

/// <summary>
/// Fixture instance for account test class
/// Contain 2 account for Client, Admin type
/// </summary>
public class AccountEntityTestFixture : IDisposable
{
	public string Email = "test@mail.com";
	public string Password = "hello123";

	public UserAccount TestClientAccount => UserAccount.Create(Email, Password, UserAccountRole.Client);
	public UserAccount TestAdminAccount => UserAccount.Create(Email, Password, UserAccountRole.Admin);


	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}
}
