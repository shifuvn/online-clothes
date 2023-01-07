using FluentValidation;

namespace OnlineClothes.Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public string? ContactEmail { get; set; }
}

public class CreateBrandCommandValidation : AbstractValidator<CreateBrandCommand>
{
	public CreateBrandCommandValidation()
	{
		RuleFor(q => q.Name).NotEmpty();
	}
}
