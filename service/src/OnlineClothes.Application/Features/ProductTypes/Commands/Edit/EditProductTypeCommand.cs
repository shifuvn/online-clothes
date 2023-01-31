namespace OnlineClothes.Application.Features.ProductTypes.Commands.Edit;

public class EditProductTypeCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
}
