using OnlineClothes.Application.Features.ProductTypes.Commands.Create;
using OnlineClothes.Application.Features.ProductTypes.Commands.Delete;
using OnlineClothes.Application.Features.ProductTypes.Commands.Edit;
using OnlineClothes.Application.Features.ProductTypes.Queries.All;
using OnlineClothes.Application.Features.ProductTypes.Queries.Paging;
using OnlineClothes.Application.Features.ProductTypes.Queries.Single;

namespace OnlineClothes.Api.Controllers.V1;

public class ProductTypesController : ApiV1ControllerBase
{
	public ProductTypesController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet]
	public async Task<IActionResult> Paging([FromQuery] GetPagingProductTypeQuery request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> Single([FromRoute] int id)
	{
		return HandleApiResponse(await Mediator.Send(new GetSingleProductTypeQuery { Id = id }));
	}

	[HttpGet("all")]
	public async Task<IActionResult> All()
	{
		return HandleApiResponse(await Mediator.Send(new GetAllProductTypeQuery()));
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateProductTypeCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut]
	public async Task<IActionResult> Edit([FromBody] EditProductTypeCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete([FromRoute] int id)
	{
		return HandleApiResponse(await Mediator.Send(new DeleteProductTypeCommand { Id = id }));
	}
}
