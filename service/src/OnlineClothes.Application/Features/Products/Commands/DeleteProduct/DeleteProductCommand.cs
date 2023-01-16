namespace OnlineClothes.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public DeleteProductCommand(int id)
	{
		Id = id;
	}

	public int Id { get; set; }
}
