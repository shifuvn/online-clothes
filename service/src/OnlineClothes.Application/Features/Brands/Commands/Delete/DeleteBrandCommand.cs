namespace OnlineClothes.Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public DeleteBrandCommand(int id)
	{
		Id = id;
	}

	public int Id { get; init; }
}
