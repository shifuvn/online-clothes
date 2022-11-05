using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Accounts.Commands.SignIn;
using OnlineClothes.Application.Features.Accounts.Commands.SignUp;

namespace OnlineClothes.Api.Controllers;

public class AccountsController : ApiV1ControllerBase
{
	public AccountsController(IMediator mediator) : base(mediator)
	{
	}

	[HttpPost("sign-up")]
	[AllowAnonymous]
	public async Task<IActionResult> SignUp([FromBody] SignUpAccountCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	public async Task<IActionResult> SignIn([FromBody] SignInAccountCommand command,
		CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(command, cancellationToken));
	}

	[HttpGet("test-authorize")]
	[Authorize]
	public IActionResult TestAuthorize()
	{
		return Ok();
	}
}
