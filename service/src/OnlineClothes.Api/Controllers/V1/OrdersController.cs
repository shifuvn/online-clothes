using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Orders.Commands.Cancel;
using OnlineClothes.Application.Features.Orders.Commands.Checkout;
using OnlineClothes.Application.Features.Orders.Commands.Delivery;
using OnlineClothes.Application.Features.Orders.Commands.Success;
using OnlineClothes.Application.Features.Orders.Queries.Detail;
using OnlineClothes.Application.Features.Orders.Queries.Paging;

namespace OnlineClothes.Api.Controllers.V1;

public class OrdersController : ApiV1ControllerBase
{
	public OrdersController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> Detail(int id)
	{
		return HandleApiResponse(await Mediator.Send(new OrderDetailQuery(id)));
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Paging([FromQuery] OrderListingQuery request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPost("checkout")]
	[Authorize]
	public async Task<IActionResult> Checkout([FromBody] CheckoutOrderCommand command)
	{
		return HandleApiResponse(await Mediator.Send(command));
	}

	[HttpPut("{orderId}/delivery")]
	//[Authorize(Roles = nameof(AccountRole.Admin))]
	public async Task<IActionResult> Delivery(int orderId)
	{
		return HandleApiResponse(await Mediator.Send(new DeliveryOrderCommand(orderId)));
	}

	[HttpPut("{orderId}/success")]
	//[Authorize(Roles = nameof(AccountRole.Admin))]
	public async Task<IActionResult> Success(int orderId)
	{
		return HandleApiResponse(await Mediator.Send(new SuccessOrderCommand(orderId)));
	}

	[HttpPut("{orderId}/cancel")]
	public async Task<IActionResult> Cancel(int orderId)
	{
		return HandleApiResponse(await Mediator.Send(new CancelOrderCommand(orderId)));
	}
}
