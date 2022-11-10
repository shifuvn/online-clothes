using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Order.Commands.Checkout;
using OnlineClothes.Application.Features.Order.Queries.Detail;
using OnlineClothes.Application.Features.Order.Queries.Listing;

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

	[HttpGet("{productId}")]
	[Authorize]
	public async Task<IActionResult> Detail(string productId, CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(new OrderDetailQuery(productId), cancellationToken));
	}

	[HttpPost("checkout")]
	[Authorize]
	public async Task<IActionResult> Checkout([FromBody] CheckoutOrderCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}
}
