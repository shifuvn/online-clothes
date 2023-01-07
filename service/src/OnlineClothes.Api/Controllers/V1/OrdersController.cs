using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Orders.Commands.Cancel;
using OnlineClothes.Application.Features.Orders.Commands.Checkout;
using OnlineClothes.Application.Features.Orders.Commands.Delivery;
using OnlineClothes.Application.Features.Orders.Commands.Success;
using OnlineClothes.Application.Features.Orders.Queries.Detail;
using OnlineClothes.Domain.Common;

namespace OnlineClothes.Api.Controllers.V1;

public class OrdersController : ApiV1ControllerBase
{
	public OrdersController(IMediator mediator) : base(mediator)
	{
	}

	//[HttpGet]
	//[Authorize]
	//public async Task<IActionResult> Listing(CancellationToken cancellationToken = default)
	//{
	//	return HandleApiResponse(await Mediator.Send(new OrderListingQuery(), cancellationToken));
	//}

	[HttpGet("{orderId}")]
	[Authorize]
	public async Task<IActionResult> Detail(string orderId, CancellationToken cancellationToken = default)
	{
		return HandleApiResponse(await Mediator.Send(new OrderDetailQuery(orderId), cancellationToken));
	}

	[HttpPost("checkout")]
	[Authorize]
	public async Task<IActionResult> Checkout([FromBody] CheckoutOrderCommand command)
	{
		return HandleApiResponse(await Mediator.Send(command));
	}

	[HttpPut("{orderId}/delivery")]
	[Authorize(Roles = nameof(AccountRole.Admin))]
	public async Task<IActionResult> Delivery(string orderId)
	{
		return HandleApiResponse(await Mediator.Send(new DeliveryOrderCommand(orderId)));
	}

	[HttpPut("{orderId}/success")]
	[Authorize(Roles = nameof(AccountRole.Admin))]
	public async Task<IActionResult> Success(string orderId)
	{
		return HandleApiResponse(await Mediator.Send(new SuccessOrderCommand(orderId)));
	}

	[HttpPut("{orderId}/cancel")]
	public async Task<IActionResult> Cancel(string orderId)
	{
		return HandleApiResponse(await Mediator.Send(new CancelOrderCommand(orderId)));
	}
}
