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
	public async Task<IActionResult> GetSingleCategory(int id)
	{
		return HandleApiResponse(await Mediator.Send(new GetSingleCategoryQuery(id)));
	}

	[HttpGet]
	public async Task<IActionResult> GetPagingCategory([FromQuery] PagingRequest pageRequest)
	{
		return HandleApiResponse(await Mediator.Send(new GetPagingCategoryQuery(pageRequest)));
	}

	[HttpPost]
	public async Task<IActionResult> CreateCategory([FromBody] CreateNewCategoryCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpPut]
	public async Task<IActionResult> EditCategory([FromBody] EditCategoryCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteCategory(int id)
	{
		return HandleApiResponse(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
	}
}
