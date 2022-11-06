using OnlineClothes.Domain.Attributes;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities.Common;

namespace OnlineClothes.Domain.Entities;

[BsonCollection("accountUsers")]
public class AccountUser : RootDocumentBase
{
	public AccountUser()
	{
	}

	public AccountUser(string email, string hashedPassword, string firstName, string lastName, string role)
	{
		Email = email;
		NormalizeEmail = email.ToUpper();
		HashedPassword = hashedPassword;
		FirstName = firstName;
		LastName = lastName;
		Role = role;
	}

	public string Email { get; set; } = null!;
	public string NormalizeEmail { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string HashedPassword { get; set; } = null!;
	public string Role { get; set; } = null!;
	public string ImageUrl { get; set; } = null!;
	public bool IsActivated { get; set; }
	public DateTime LastLogin { get; set; }

	public static AccountUser Create(string email, string rawPassword, AccountUserFullName fullName,
		UserAccountRole role)
	{
		var hashedPassword = PasswordHasher.Hash(rawPassword);

		return new AccountUser(email, hashedPassword, fullName.FirstName, fullName.LastName, role.ToString());
	}

	public bool VerifyPassword(string providedPassword)
	{
		var result = PasswordHasher.Verify(HashedPassword, providedPassword);
		return result switch
		{
			PasswordVerificationResult.Success => true,
			PasswordVerificationResult.Failed => false,
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	public void Activate()
	{
		IsActivated = true;
	}

	public void Deactivate()
	{
		IsActivated = false;
	}

	public bool IsValid()
	{
		return IsActivated;
	}

	public bool HasRole(UserAccountRole providedRole)
	{
		return Role.Equals(providedRole.ToString());
	}
}
