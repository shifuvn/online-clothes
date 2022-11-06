using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Accounts.Commands.ChangePassword;
using OnlineClothes.Application.Features.Accounts.Commands.Reset;
using OnlineClothes.Application.Features.Accounts.Commands.SignIn;
using OnlineClothes.Application.Features.Accounts.Commands.SignUp;
using OnlineClothes.Application.Features.Accounts.Queries.Activate;
using OnlineClothes.Application.Features.Accounts.Queries.Recovery;

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
	public async Task<IActionResult> Activate([FromQuery] ActivateQuery query)
	{
		return ApiResponse(await Mediator.Send(query));
	}

	[HttpGet("recovery")]
	[AllowAnonymous]
	public async Task<IActionResult> Recovery([FromQuery] RecoveryQuery query)
	{
		return ApiResponse(await Mediator.Send(query));
	}

	[HttpPost("change-password")]
	[AllowAnonymous]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}

	[HttpPost("reset")]
	[AllowAnonymous]
	public async Task<IActionResult> Reset([FromBody] ResetCommand command)
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
