using System.ComponentModel;
using System.Text.RegularExpressions;
using FluentValidation;
using OnlineClothes.Application.Commons;

namespace OnlineClothes.Application.Features.Products.Commands.CreateNewSku;

public class CreateSkuCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int ProductId { get; set; }
	public string Sku { get; set; } = null!;
	[DefaultValue(0)] public decimal AddOnPrice { get; set; }
	[DefaultValue(0)] public int InStock { get; set; }
	[DefaultValue(ClotheSizeType.NoSize)] public ClotheSizeType Size { get; set; }

	// TODO: image
}

public class CreateSkuCommandValidation : AbstractValidator<CreateSkuCommand>
{
	public CreateSkuCommandValidation()
	{
		RuleFor(q => q.Sku)
			.Matches(new Regex(RegexPattern.ValidSku)).WithMessage("Sku chỉ sử dụng các kí tự [a-z], [0-9] và `-`");
		RuleFor(q => q.AddOnPrice).GreaterThanOrEqualTo(0);
		RuleFor(q => q.InStock).GreaterThanOrEqualTo(0);
		RuleFor(q => q.ProductId).GreaterThanOrEqualTo(0);
	}
}
