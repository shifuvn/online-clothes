using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;

namespace OnlineClothes.Infrastructure.Services.UserContext;

internal sealed class UserContext : IUserContext
{
	private readonly ClaimsPrincipal _claims;

	public UserContext(IHttpContextAccessor httpContextAccessor)
	{
		_claims = httpContextAccessor.HttpContext?.User!;
	}

	public string GetNameIdentifier()
	{
		return _claims.FindFirstValue(ClaimTypes.NameIdentifier);
	}

	public string GetAccountEmail()
	{
		return _claims.FindFirstValue(ClaimTypes.Email);
	}

	public string GetRole()
	{
		return _claims.FindFirstValue(ClaimTypes.Role);
	}
}
