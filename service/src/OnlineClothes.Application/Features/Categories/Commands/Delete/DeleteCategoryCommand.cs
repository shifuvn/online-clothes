using FluentValidation;

namespace OnlineClothes.Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int Id { get; init; }
}

public sealed class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
{
	public DeleteCategoryCommandValidation()
	{
		RuleFor(q => q.Id).NotEmpty();
	}
}
