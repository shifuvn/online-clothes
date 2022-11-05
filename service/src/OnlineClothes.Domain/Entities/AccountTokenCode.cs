using OnlineClothes.Domain.Attributes;

namespace OnlineClothes.Domain.Entities;

[BsonCollection("accountTokenCodes")]
public class AccountTokenCode : MongoEntityBase
{
	public AccountTokenCode(string email, AccountTokenType type, TimeSpan lifeTimeSpan)
	{
		Email = email;
		TokenType = type;
		ExpiredAtStamp = (ulong)DateTimeOffset.UtcNow.Add(lifeTimeSpan).ToUnixTimeSeconds();
		TokenCode = Guid.NewGuid().ToString();
	}

	public AccountTokenType TokenType { get; set; }
	public string Email { get; set; }
	public string TokenCode { get; set; }
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
