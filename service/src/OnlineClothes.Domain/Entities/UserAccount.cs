using OnlineClothes.Domain.Common;

namespace OnlineClothes.Domain.Entities;

public class UserAccount : MongoEntityBase
{
	public UserAccount()
	{
	}

	public UserAccount(string email, string hashedPassword, string role)
	{
		Email = email;
		NormalizeEmail = email.ToUpper();
		HashedPassword = hashedPassword;
		Role = role;
	}

	public string Email { get; set; } = null!;
	public string NormalizeEmail { get; set; } = null!;
	public string HashedPassword { get; set; } = null!;
	public string Role { get; set; } = null!;
	public string ImageUrl { get; set; } = null!;
	public bool IsActivated { get; set; }
	public DateTime LastLogin { get; set; }

	public static UserAccount Create(string email, string rawPassword, UserAccountRole role)
	{
		var hashedPassword = PasswordHasher.Hash(rawPassword);

		return new UserAccount(email, hashedPassword, role.ToString());
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
