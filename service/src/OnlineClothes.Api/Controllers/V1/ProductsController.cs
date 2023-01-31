using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Products.Commands.Create;
using OnlineClothes.Application.Features.Products.Commands.DeleteProduct;
using OnlineClothes.Application.Features.Products.Commands.EditProductInfo;
using OnlineClothes.Application.Features.Products.Commands.PromoteThumbnail;
using OnlineClothes.Application.Features.Products.Commands.RestoreProduct;
using OnlineClothes.Application.Features.Products.Queries.Paging;
using OnlineClothes.Application.Features.Products.Queries.ProductImages;

namespace OnlineClothes.Api.Controllers.V1;

public class ProductsController : ApiV1ControllerBase
{
	public ProductsController(IMediator mediator) : base(mediator)
	{
	}


	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> GetPaging([FromQuery] GetPagingProductQuery query)
	{
		return HandleApiResponse(await Mediator.Send(query));
	}


	[HttpGet("{productId:int}/images")]
	[AllowAnonymous]
	public async Task<IActionResult> GetProductImage([FromRoute] int productId)
	{
		return HandleApiResponse(await Mediator.Send(new GetProductImageQuery { Id = productId }));
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}


	[HttpPut]
	public async Task<IActionResult> EditProduct([FromBody] EditProductCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}


	[HttpPut("{id:int}/restore")]
	public async Task<IActionResult> RestoreProduct(int id)
	{
		return HandleApiResponse(await Mediator.Send(new RestoreProductCommand(id)));
	}


	[HttpPut("thumbnail/promote")]
	public async Task<IActionResult> PromoteThumbnailImage([FromBody] PromoteProductThumbnailCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}


	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		return HandleApiResponse(await Mediator.Send(new DeleteProductCommand(id)));
	}
}
