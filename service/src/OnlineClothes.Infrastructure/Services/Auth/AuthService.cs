using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Services.Auth.Abstracts;
using OnlineClothes.Support.Utilities;

namespace OnlineClothes.Infrastructure.Services.Auth;

internal sealed class AuthService : IAuthService
{
	private readonly AuthConfiguration _authConfiguration;
	private readonly ILogger<AuthService> _logger;

	public AuthService(ILogger<AuthService> logger, IOptions<AuthConfiguration> authConfigurationOption)
	{
		_logger = logger;
		_authConfiguration = authConfigurationOption.Value;
	}

	public string CreateJwtAccessToken(AccountUser account)
	{
		// credential & using SHA256
		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.Secret));
		var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


		// describe token
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(CreateClaims(account)),
			Expires = DateTime.Now.Add(TimeSpan.FromMinutes(_authConfiguration.ExpirationInMinutes)),
			SigningCredentials = credentials,
			Issuer = _authConfiguration.Issuer,
			Audience = _authConfiguration.Audience
		};

		// create token
		var tokenHandler = new JwtSecurityTokenHandler();
		var jwt = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
		var token = tokenHandler.WriteToken(jwt);

		return token;
	}

	public List<Claim> ValidateJwtToken(string jwt)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		if (tokenHandler.ReadToken(jwt) is not JwtSecurityToken)
		{
			return Util.Array.EmptyList<Claim>();
		}

		var symmetricKey = Encoding.UTF8.GetBytes(_authConfiguration.Secret);

		var validationParameters = new TokenValidationParameters
		{
			RequireExpirationTime = true,
			ValidateIssuer = false,
			ValidateAudience = false,
			IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
		};

		var principal = tokenHandler.ValidateToken(jwt, validationParameters, out var securityToken);

		return principal.Claims.ToList();
	}

	private IEnumerable<Claim> CreateClaims(AccountUser account)
	{
		var logRequestInfo = "{" + $"{account.Id} {account.Email} {account.Role}" + "}";
		_logger.LogInformation("Request for access token -- {Info}", logRequestInfo);

		return new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, account.Id),
			new(ClaimTypes.Email, account.Email),
			new(ClaimTypes.Role, account.Role)
		};
	}
}
