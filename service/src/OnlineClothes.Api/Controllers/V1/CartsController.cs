using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Carts.Commands.AddItem;
using OnlineClothes.Application.Features.Carts.Commands.RemoveItem;
using OnlineClothes.Application.Features.Carts.Queries.GetInfo;

namespace OnlineClothes.Api.Controllers.V1;

[Authorize]
public class CartsController : ApiV1ControllerBase
{
	public CartsController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		return HandleApiResponse(await Mediator.Send(new GetCartInfoQuery()));
	}

	[HttpPut("{productSku}/add-item/{quantity:int}")]
	public async Task<IActionResult> AddItem(string productSku, int quantity = 1)
	{
		return HandleApiResponse(await Mediator.Send(new AddCartItemCommand
		{
			ProductSku = productSku,
			Quantity = quantity
		}));
	}

	[HttpPut("{productId}/remove-item/{quantity:int}")]
	public async Task<IActionResult> RemoveItem(string productId, int quantity = 1)
	{
		return HandleApiResponse(await Mediator.Send(new RemoveCartItemCommand
		{
			ProductSku = productId,
			Quantity = quantity
		}));
	}
}
