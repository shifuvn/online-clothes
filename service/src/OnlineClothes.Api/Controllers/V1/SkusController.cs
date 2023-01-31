using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Images.Commands.DeleteSkuImage;
using OnlineClothes.Application.Features.Skus.Commands.CreateNewSku;
using OnlineClothes.Application.Features.Skus.Commands.DeleteSku;
using OnlineClothes.Application.Features.Skus.Commands.EditSkuInfo;
using OnlineClothes.Application.Features.Skus.Commands.ImportSku;
using OnlineClothes.Application.Features.Skus.Commands.RestoreSku;
using OnlineClothes.Application.Features.Skus.Queries.Detail;
using OnlineClothes.Application.Features.Skus.Queries.Paging;

namespace OnlineClothes.Api.Controllers.V1;

public class SkusController : ApiV1ControllerBase
{
	public SkusController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> GetPagingSku([FromQuery] GetPagingSkuQuery request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpGet("{sku}")]
	[AllowAnonymous]
	public async Task<IActionResult> GetDetail(string sku)
	{
		return HandleApiResponse(await Mediator.Send(new GetSkuDetailQuery(sku)));
	}

	[HttpPost]
	public async Task<IActionResult> CreateSku([FromForm] CreateSkuCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut]
	public async Task<IActionResult> EditSku([FromForm] EditSkuInfoCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}


	[HttpPut("import")]
	public async Task<IActionResult> ImportStock([FromBody] ImportSkuStockCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}


	[HttpPut("{sku}/restore")]
	public async Task<IActionResult> RestoreSku(string sku)
	{
		return HandleApiResponse(await Mediator.Send(new RestoreSkuCommand(sku)));
	}


	//[HttpPut("replace-image")]
	//public async Task<IActionResult> ReplaceSkuImage([FromForm] ReplaceSkuImageCommand request)
	//{
	//	return HandleApiResponse(await Mediator.Send(request));
	//}

	[HttpDelete("{sku}")]
	public async Task<IActionResult> DeleteSku(string sku)
	{
		return HandleApiResponse(await Mediator.Send(new DisableSkuCommand(sku)));
	}

	[HttpDelete("{sku}/image")]
	public async Task<IActionResult> DeleteSkuImage(string sku)
	{
		return HandleApiResponse(await Mediator.Send(new DeleteSkuImageCommand { Sku = sku }));
	}
}
