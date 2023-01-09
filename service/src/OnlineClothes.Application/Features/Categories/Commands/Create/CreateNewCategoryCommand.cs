using FluentValidation;

namespace OnlineClothes.Application.Features.Categories.Commands.Create;

public class CreateNewCategoryCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public CreateNewCategoryCommand(string name, string? description)
	{
		Name = name;
		Description = description;
	}

	public string Name { get; init; }
	public string? Description { get; init; }
}

public sealed class CreateNewCategoryCommandValidation : AbstractValidator<CreateNewCategoryCommand>
{
	public CreateNewCategoryCommandValidation()
	{
		RuleFor(q => q.Name)
			.NotEmpty().WithMessage("Tên không được để trống");
	}
}
