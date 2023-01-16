using FluentValidation;

namespace OnlineClothes.Application.Features.Categories.Commands.Edit;

public class EditCategoryCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int Id { get; init; }
	public string Name { get; init; } = null!;
	public string? Description { get; init; }
}

public sealed class EditCategoryCommandValidation : AbstractValidator<EditCategoryCommand>
{
	public EditCategoryCommandValidation()
	{
		RuleFor(q => q.Id)
			.NotEmpty();
		RuleFor(q => q.Name)
			.NotEmpty().WithMessage("Tên không được trống");
	}
}
