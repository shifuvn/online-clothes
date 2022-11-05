namespace OnlineClothes.Infrastructure.Configurations;

public class AuthConfiguration
{
	public List<string> ValidIssuers { get; init; }
	public List<string> ValidAudiences { get; init; }
	public string Issuer { get; init; }
	public string Audience { get; init; }
	public string Secret { get; set; }
	public uint ExpirationInMinutes { get; set; }
	public bool ValidateIssuer { get; init; }
	public bool ValidateAudience { get; init; }
}
