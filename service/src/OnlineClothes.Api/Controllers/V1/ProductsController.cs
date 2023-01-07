using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Products.Commands.CreateNewProductSeri;
using OnlineClothes.Application.Features.Products.Commands.CreateNewSku;
using OnlineClothes.Application.Features.Products.Commands.DeleteProduct;
using OnlineClothes.Application.Features.Products.Commands.DeleteSku;
using OnlineClothes.Application.Features.Products.Commands.EditProductInfo;
using OnlineClothes.Application.Features.Products.Commands.EditSkuInfo;
using OnlineClothes.Application.Features.Products.Commands.ImportSku;
using OnlineClothes.Application.Features.Products.Commands.RestoreProduct;
using OnlineClothes.Application.Features.Products.Commands.RestoreSku;
using OnlineClothes.Application.Features.Products.Commands.UploadImage;
using OnlineClothes.Application.Features.Products.Queries.Detail;
using OnlineClothes.Application.Features.Products.Queries.Paging;
using OnlineClothes.Domain.Common;

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

	[HttpGet("{sku}")]
	[AllowAnonymous]
	public async Task<IActionResult> GetDetail(string sku)
	{
		return HandleApiResponse(await Mediator.Send(new GetSkuDetailQuery(sku)));
	}

	[HttpPost("product")]
	public async Task<IActionResult> CreateProduct([FromForm] CreateNewProductCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPost("sku")]
	public async Task<IActionResult> CreateSku([FromForm] CreateSkuCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut("product/edit")]
	public async Task<IActionResult> EditProduct([FromBody] EditProductCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut("sku/edit")]
	public async Task<IActionResult> EditSku([FromForm] EditSkuInfoCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut("sku/import")]
	public async Task<IActionResult> ImportStock([FromBody] ImportSkuStockCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut("{id}/upload-image")]
	[Authorize(Roles = nameof(AccountRole.Admin))]
	public async Task<IActionResult> UploadImage(string id, [FromForm] IFormFile file)
	{
		return HandleApiResponse(await Mediator.Send(new UploadProductImageCommand(id, file)));
	}

	[HttpPut("product/{id:int}/restore")]
	public async Task<IActionResult> RestoreProduct(int id)
	{
		return HandleApiResponse(await Mediator.Send(new RestoreProductCommand(id)));
	}

	[HttpPut("sku/{sku}/restore")]
	public async Task<IActionResult> RestoreSku(string sku)
	{
		return HandleApiResponse(await Mediator.Send(new RestoreSkuCommand(sku)));
	}

	[HttpDelete("product/{id:int}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		return HandleApiResponse(await Mediator.Send(new DeleteProductCommand(id)));
	}

	[HttpDelete("sku/{sku}")]
	public async Task<IActionResult> DeleteSku(string sku)
	{
		return HandleApiResponse(await Mediator.Send(new DisableSkuCommand(sku)));
	}
}
