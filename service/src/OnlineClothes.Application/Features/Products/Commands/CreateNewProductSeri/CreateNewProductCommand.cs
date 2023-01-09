using System.ComponentModel;
using System.Text.RegularExpressions;
using FluentValidation;
using OnlineClothes.Application.Commons;
using OnlineClothes.Application.Persistence.Schemas.Products;

namespace OnlineClothes.Application.Features.Products.Commands.CreateNewProductSeri;

public class CreateNewProductCommand : PutProductInRepoObject, IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	// default sku of product
	public string Sku { get; set; } = null!;
	[DefaultValue(0)] public int SkuInStock { get; set; }
	[DefaultValue(0)] public decimal SkuAddOnPrice { get; set; }
	public ClotheSizeType SkuSize { get; set; }
}

public class CreateNewProductCommandValidation : AbstractValidator<CreateNewProductCommand>
{
	public CreateNewProductCommandValidation()
	{
		RuleFor(q => q.Sku)
			.Matches(new Regex(RegexPattern.ValidSku))
			.WithMessage("Sku chỉ sử dụng các kí tự [a-z], [0-9] và `-`");
		RuleFor(q => q.Name).NotEmpty();
		RuleFor(q => q.Price).GreaterThanOrEqualTo(0);
	}
}
