namespace OnlineClothes.Application.Features.ProductTypes.Queries.Single;

public class GetSingleProductTypeQueryViewModel : ProductTypeDto
{
	public GetSingleProductTypeQueryViewModel(int id, string name) : base(id, name)
	{
	}

	public GetSingleProductTypeQueryViewModel(ProductType domain) : base(domain)
	{
	}
}
