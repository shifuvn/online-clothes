using FluentValidation;

namespace OnlineClothes.Application.Features.ProductTypes.Commands.Create;

public class CreateProductTypeCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Name { get; set; } = null!;
}

public class CreateProductTypeCommandValidation : AbstractValidator<CreateProductTypeCommand>
{
	public CreateProductTypeCommandValidation()
	{
		RuleFor(q => q.Name.Trim())
			.NotEmpty().WithMessage("Name không được để trống");
	}
}
