namespace OnlineClothes.Domain.Entities.Aggregate;

public class AccountTokenCode : EntityBase
{
	public AccountTokenCode()
	{
	}

	public AccountTokenCode(string email, AccountTokenType type, TimeSpan lifeTimeSpan) : this()
	{
		Email = email;
		TokenType = type;
		ExpiredAtStamp = (ulong)DateTimeOffset.UtcNow.Add(lifeTimeSpan).ToUnixTimeSeconds();
		TokenCode = Guid.NewGuid().ToString();
	}

	public AccountTokenType TokenType { get; set; }
	public string Email { get; set; } = null!;
	public string TokenCode { get; set; } = null!;
	public ulong ExpiredAtStamp { get; set; }

	public bool IsValid()
	{
		return (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds() <= ExpiredAtStamp;
	}
}

public enum AccountTokenType
{
	Verification,
	ResetPassword
}
