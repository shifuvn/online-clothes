namespace OnlineClothes.Application.Services.Auth.Models;

public class AuthConfiguration
{
	public List<string>? ValidIssuers { get; init; }
	public List<string>? ValidAudiences { get; init; }
	public string? Issuer { get; init; }
	public string? Audience { get; init; }
	public string Secret { get; set; } = null!;
	public uint ExpirationInMinutes { get; set; } = 120;
	public bool ValidateIssuer { get; init; }
	public bool ValidateAudience { get; init; }
}
