using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Order.Commands.Cancel;
using OnlineClothes.Application.Features.Order.Commands.Checkout;
using OnlineClothes.Application.Features.Order.Commands.Delivery;
using OnlineClothes.Application.Features.Order.Commands.Success;
using OnlineClothes.Application.Features.Order.Queries.Detail;
using OnlineClothes.Application.Features.Order.Queries.Listing;
using OnlineClothes.Domain.Common;

namespace OnlineClothes.Api.Controllers.V1;

public class OrdersController : ApiV1ControllerBase
{
	public OrdersController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Listing(CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(new OrderListingQuery(), cancellationToken));
	}

	[HttpGet("{orderId}")]
	[Authorize]
	public async Task<IActionResult> Detail(string orderId, CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(new OrderDetailQuery(orderId), cancellationToken));
	}

	[HttpPost("checkout")]
	[Authorize]
	public async Task<IActionResult> Checkout([FromBody] CheckoutOrderCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}

	[HttpPut("{orderId}/delivery")]
	[Authorize(Roles = nameof(AccountRole.Admin))]
	public async Task<IActionResult> Delivery(string orderId)
	{
		return ApiResponse(await Mediator.Send(new DeliveryOrderCommand(orderId)));
	}

	[HttpPut("{orderId}/success")]
	[Authorize(Roles = nameof(AccountRole.Admin))]
	public async Task<IActionResult> Success(string orderId)
	{
		return ApiResponse(await Mediator.Send(new SuccessOrderCommand(orderId)));
	}

	[HttpPut("{orderId}/cancel")]
	public async Task<IActionResult> Cancel(string orderId)
	{
		return ApiResponse(await Mediator.Send(new CancelOrderCommand(orderId)));
	}
}