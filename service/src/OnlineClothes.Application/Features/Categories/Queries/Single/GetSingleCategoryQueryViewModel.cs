namespace OnlineClothes.Application.Features.Categories.Queries.Single;

public class GetSingleCategoryQueryViewModel : CategoryDto
{
	public GetSingleCategoryQueryViewModel(int id, string name, string? description) : base(id, name, description)
	{
	}

	public GetSingleCategoryQueryViewModel(Category domain) : base(domain)
	{
	}
}
