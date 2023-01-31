using FluentValidation;

namespace OnlineClothes.Application.Features.Categories.Queries.Single;

public class GetSingleCategoryQuery : IRequest<JsonApiResponse<GetSingleCategoryQueryViewModel>>
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
