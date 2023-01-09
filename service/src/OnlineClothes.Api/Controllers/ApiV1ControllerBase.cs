using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Api.Controllers;

[Route("api/v1/[controller]")]
public abstract class ApiV1ControllerBase : ControllerBase
{
	protected readonly IMediator Mediator;

	protected ApiV1ControllerBase(IMediator mediator)
	{
		Mediator = mediator;
	}

	protected ActionResult HandleApiResponse<T>(JsonApiResponse<T> responseApi, string? redirect = null) where T : class
	{
		if (!responseApi.IsError)
		{
			return responseApi.Status switch
			{
				//StatusCodes.Status302Found => Redirect(redirect),
				_ => StatusCode(responseApi.Status, responseApi)
			};
		}

		return StatusCode(responseApi.Status, responseApi);
	}
}
