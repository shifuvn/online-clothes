using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Accounts.Commands.ChangePassword;
using OnlineClothes.Application.Features.Accounts.Commands.Reset;
using OnlineClothes.Application.Features.Accounts.Commands.SignIn;
using OnlineClothes.Application.Features.Accounts.Commands.SignUp;
using OnlineClothes.Application.Features.Accounts.Queries.Activate;
using OnlineClothes.Application.Features.Accounts.Queries.Recovery;
using OnlineClothes.Application.Features.Accounts.Queries.ResendActivation;

namespace OnlineClothes.Api.Controllers.V1;

public class AccountsController : ApiV1ControllerBase
{
	public AccountsController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet("recovery")]
	[AllowAnonymous]
	public async Task<IActionResult> Recovery([FromQuery] RecoveryQuery query)
	{
		return HandleApiResponse(await Mediator.Send(query));
	}

	[HttpGet("activate")]
	[AllowAnonymous]
	public async Task<IActionResult> Activate([FromQuery] ActivateQuery query)
	{
		return HandleApiResponse(await Mediator.Send(query));
	}

	[HttpGet("resend-activate")]
	[AllowAnonymous]
	public async Task<IActionResult> ResendActivate([FromQuery] ResendActivationQuery query)
	{
		return HandleApiResponse(await Mediator.Send(query));
	}


	[HttpPost("sign-up")]
	[AllowAnonymous]
	public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
	{
		return HandleApiResponse(await Mediator.Send(command));
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	public async Task<IActionResult> SignIn([FromBody] SignInCommand command,
		CancellationToken cancellationToken = default)
	{
		return HandleApiResponse(await Mediator.Send(command, cancellationToken));
	}

	[HttpPost("change-password")]
	[AllowAnonymous]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
	{
		return HandleApiResponse(await Mediator.Send(command));
	}


	[HttpPost("reset")]
	[AllowAnonymous]
	public async Task<IActionResult> Reset([FromBody] ResetCommand command)
	{
		return HandleApiResponse(await Mediator.Send(command));
	}

	[HttpGet("test-authorize")]
	[Authorize]
	public IActionResult TestAuthorize()
	{
		return Ok();
	}
}
