using FluentValidation;

namespace OnlineClothes.Application.Features.Products.Commands.EditSkuInfo;

public class EditSkuInfoCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Sku { get; set; } = null!;
	public int InStock { get; set; }
	public decimal AddOnPrice { get; set; }
	public ClotheSizeType Size { get; set; }
	public bool IsDeleted { get; set; }
}

public class EditSkuInfoCommandValidation : AbstractValidator<EditSkuInfoCommand>
{
	public EditSkuInfoCommandValidation()
	{
		RuleFor(q => q.InStock).GreaterThanOrEqualTo(0);
		RuleFor(q => q.AddOnPrice).GreaterThanOrEqualTo(0);
	}
}
