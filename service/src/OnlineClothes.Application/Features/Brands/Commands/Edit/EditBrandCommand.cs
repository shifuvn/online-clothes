using FluentValidation;

namespace OnlineClothes.Application.Features.Brands.Commands.Edit;

public class EditBrandCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int Id { get; init; }
	public string Name { get; init; } = null!;
	public string? Description { get; init; }
	public string? ContactEmail { get; init; }
}

public class EditBrandCommandValidation : AbstractValidator<EditBrandCommand>
{
	public EditBrandCommandValidation()
	{
		RuleFor(q => q.Id).NotEmpty();
		RuleFor(q => q.Name).NotEmpty();
	}
}
