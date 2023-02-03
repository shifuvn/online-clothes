namespace OnlineClothes.Application.Features.Brands.Queries.Single;

public class GetSingleBrandQueryViewModel : BrandDto
{
	public GetSingleBrandQueryViewModel(int id, string name, string? description, string? contactEmail) : base(id, name,
		description, contactEmail)
	{
	}

	public GetSingleBrandQueryViewModel(Brand domain) : base(domain)
	{
	}
}
