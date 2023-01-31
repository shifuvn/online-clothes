using OnlineClothes.Application.Features.Categories.Commands.Create;
using OnlineClothes.Application.Features.Categories.Commands.Delete;
using OnlineClothes.Application.Features.Categories.Commands.Edit;
using OnlineClothes.Application.Features.Categories.Queries.Paging;
using OnlineClothes.Application.Features.Categories.Queries.Single;

namespace OnlineClothes.Api.Controllers.V1;

public class CategoriesController : ApiV1ControllerBase
{
	public CategoriesController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetSingle(int id)
	{
		return HandleApiResponse(await Mediator.Send(new GetSingleCategoryQuery(id)));
	}

	[HttpGet]
	public async Task<IActionResult> GetPaging([FromQuery] PagingRequest pageRequest)
	{
		return HandleApiResponse(await Mediator.Send(new GetPagingCategoryQuery(pageRequest)));
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut]
	public async Task<IActionResult> Edit([FromBody] EditCategoryCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		return HandleApiResponse(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
	}
}
