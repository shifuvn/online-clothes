using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Accounts.Commands.Activate;
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
	public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	public async Task<IActionResult> SignIn([FromBody] SignInCommand command,
		CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(command, cancellationToken));
	}

	[HttpGet("activate")]
	[AllowAnonymous]
	public async Task<IActionResult> Activate([FromQuery] ActivateCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}

	[HttpGet("test-authorize")]
	[Authorize]
	public IActionResult TestAuthorize()
	{
		return Ok();
	}
}
