namespace OnlineClothes.Application.Features.Product.Commands.NewProduct;

public class CreateNewClotheCommandResultModel
{
	public CreateNewClotheCommandResultModel(string newProductId)
	{
		NewProductId = newProductId;
	}

	public string NewProductId { get; set; }
}
