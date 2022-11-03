using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Accounts.Commands.SignUp;

namespace OnlineClothes.Api.Controllers;

public class AccountsController : ApiV1ControllerBase
{
	public AccountsController(IMediator mediator) : base(mediator)
	{
	}

	[AllowAnonymous]
	[HttpPost("sign-up")]
	public async Task<IActionResult> SignUp(SignUpAccountCommand requestCommand)
	{
		return ApiResponse(await Mediator.Send(requestCommand));
	}
}
