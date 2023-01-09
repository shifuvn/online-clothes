namespace OnlineClothes.Domain.Entities.Aggregate;

public class AccountUser : EntityBase
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
	public string? PhoneNumber { get; set; }
	public string? Address { get; set; }
	public string? ImageUrl { get; set; } = null!;
	public bool IsActivated { get; set; }
	public DateTime LastLogin { get; set; }

	/// <summary>
	/// Create basic account.
	/// Default will create Client account
	/// </summary>
	/// <param name="email"></param>
	/// <param name="providePassword"></param>
	/// <param name="fullname"></param>
	/// <param name="role"></param>
	/// <returns></returns>
	public static AccountUser Create(string email,
		string providePassword,
		Fullname fullname,
		AccountRole role = AccountRole.Client)
	{
		var hashedPassword = PasswordHasher.Hash(providePassword);

		return new AccountUser(email, hashedPassword, fullname.FirstName, fullname.LastName,
			role.ToString());
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

	public void SetPassword(string providePassword)
	{
		HashedPassword = PasswordHasher.Hash(providePassword);
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

	public bool HasRole(AccountRole providedRole)
	{
		return Role.Equals(providedRole.ToString());
	}
}
