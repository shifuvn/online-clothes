using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Carts.Commands.UpdateItemQuantity;
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

	[HttpPut]
	public async Task<IActionResult> UpdateItem(string sku, int number = 1)
	{
		return HandleApiResponse(await Mediator.Send(new UpdateCartItemQuantityCommand(sku, number)));
	}
}
