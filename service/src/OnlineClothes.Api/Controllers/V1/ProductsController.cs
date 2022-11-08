using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Product.Commands.ImportProducts;
using OnlineClothes.Application.Features.Product.Commands.NewProduct;
using OnlineClothes.Application.Features.Product.Commands.UpdateInfo;

namespace OnlineClothes.Api.Controllers.V1;

public class ProductsController : ApiV1ControllerBase
{
	public ProductsController(IMediator mediator) : base(mediator)
	{
	}

	[HttpPost("create-new")]
	public async Task<IActionResult> CreateNew([FromBody] CreateNewClotheCommand command,
		CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(command, cancellationToken));
	}

	[HttpPut("edit/{productId}")]
	public async Task<IActionResult> Update(string productId,
		[FromBody] UpdateProductCommand.UpdateProductCommandJsonBody command)
	{
		return ApiResponse(await Mediator.Send(new UpdateProductCommand
		{
			ProductId = productId,
			Body = command
		}));
	}

	[HttpPut("import-stock/{productId}/{quantity}")]
	public async Task<IActionResult> ImportStock(string productId, uint quantity)
	{
		return ApiResponse(await Mediator.Send(new ImportProductStockCommand
		{
			ProductId = productId,
			Quantity = quantity
		}));
	}
}
