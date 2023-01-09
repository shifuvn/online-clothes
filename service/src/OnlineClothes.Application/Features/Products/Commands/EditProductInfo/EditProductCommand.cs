using FluentValidation;
using OnlineClothes.Application.Persistence.Schemas.Products;

namespace OnlineClothes.Application.Features.Products.Commands.EditProductInfo;

public class EditProductCommand : PutProductInRepoObject, IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int Id { get; set; }
}

public sealed class UpdateProductCommandValidation : AbstractValidator<EditProductCommand>
{
	public UpdateProductCommandValidation()
	{
		RuleFor(q => q.Name)
			.NotEmpty().WithMessage("Tên sản phẩm không được để trống");
		RuleFor(q => q.Price)
			.GreaterThanOrEqualTo(0);
	}
}
