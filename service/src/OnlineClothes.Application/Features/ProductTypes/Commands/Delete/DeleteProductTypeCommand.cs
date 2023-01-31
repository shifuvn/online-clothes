namespace OnlineClothes.Application.Features.ProductTypes.Commands.Delete;

public class DeleteProductTypeCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int Id { get; set; }
}
