using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Order.Commands.Checkout;

namespace OnlineClothes.Api.Controllers.V1;

public class OrdersController : ApiV1ControllerBase
{
	public OrdersController(IMediator mediator) : base(mediator)
	{
	}

	[HttpPost("checkout")]
	[Authorize]
	public async Task<IActionResult> Checkout([FromBody] CheckoutOrderCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}
}
