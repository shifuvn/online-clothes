﻿using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Domain.Entities.Common;

namespace OnlineClothes.UnitTest.Domain;

[Collection("Domain AccountUser")]
public class AccountEntityTest : IClassFixture<AccountEntityTestFixture>
{
	private readonly AccountEntityTestFixture fixture;

	public AccountEntityTest(AccountEntityTestFixture fixture)
	{
		this.fixture = fixture;
	}

	[Fact]
	[Trait("Category", "Domain_Account")]
	public void Account_CreateNew()
	{
		// arrange

		// act
		var newAccount = fixture.TestClient;

		// assert
		Assert.Equal(fixture.Email.ToUpper(), newAccount.NormalizeEmail);
		Assert.Equal("Client", newAccount.Role);
		Assert.NotEqual(fixture.Password, newAccount.NormalizeEmail);
		Assert.False(newAccount.IsActivated);
	}

	[Fact]
	[Trait("Category", "Domain_Account")]
	public void Account_Activate()
	{
		// arrange
		var newAccount = fixture.TestClient;

		// act
		newAccount.Activate();

		// assert
		Assert.True(newAccount.IsActivated);
	}

	[Fact]
	[Trait("Category", "Domain_Account")]
	public void Account_GivenClientAccount_HasClientRole()
	{
		// arrange
		var newAccount = fixture.TestClient;

		// act
		var hasRole = newAccount.HasRole(UserAccountRole.Client);

		// assert
		Assert.True(hasRole);
	}

	[Fact]
	[Trait("Category", "Domain_Account")]
	public void Account_GivenUser_ShouldVerifyPasswordTrue()
	{
		// arrange
		var newAccount = fixture.TestClient;

		// act
		var verify = newAccount.VerifyPassword(fixture.Password);

		// assert
		Assert.True(verify);
	}

	[Fact]
	[Trait("Category", "Domain_Account")]
	public void Account_GivenUser_ShouldVerifyPasswordFalse()
	{
		// arrange
		var newAccount = fixture.TestClient;

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
	public AccountUserFullName FullName = AccountUserFullName.Create("test acc");
	public string Password = "hello123";

	public AccountUser TestClient => AccountUser.Create(Email, Password, FullName, UserAccountRole.Client);
	public AccountUser TestAdmin => AccountUser.Create(Email, Password, FullName, UserAccountRole.Admin);


	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}
}
