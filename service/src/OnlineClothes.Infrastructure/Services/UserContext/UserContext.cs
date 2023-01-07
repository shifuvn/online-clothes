using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Infrastructure.Services.UserContext;

internal sealed class UserContext : IUserContext
{
	private readonly ClaimsPrincipal _claims;

	public UserContext(IHttpContextAccessor httpContextAccessor)
	{
		_claims = httpContextAccessor.HttpContext?.User!;
	}

	public int GetNameIdentifier()
	{
		var id = _claims.FindFirstValue(ClaimTypes.NameIdentifier);
		return int.Parse(id);
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
