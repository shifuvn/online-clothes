using FluentValidation;
using OnlineClothes.Application.Mapping.ViewModels;

namespace OnlineClothes.Application.Features.Categories.Queries.Single;

public class GetSingleCategoryQuery : IRequest<JsonApiResponse<CategoryViewModel>>
{
	public GetSingleCategoryQuery(int id)
	{
		Id = id;
	}

	public int Id { get; init; }
}

public sealed class GetSingleCategoryQueryValidation : AbstractValidator<GetSingleCategoryQuery>
{
	public GetSingleCategoryQueryValidation()
	{
		RuleFor(q => q.Id).NotEmpty();
	}
}
