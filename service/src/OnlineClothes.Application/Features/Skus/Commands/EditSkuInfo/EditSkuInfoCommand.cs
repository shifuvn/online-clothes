using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Features.Skus.Commands.EditSkuInfo;

public class EditSkuInfoCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Sku { get; set; } = null!;
	public int InStock { get; set; }
	public decimal AddOnPrice { get; set; }
	public ClotheSizeType Size { get; set; }
	public bool IsDeleted { get; set; }
	public IFormFile? ImageFile { get; set; }
}

public class EditSkuInfoCommandValidation : AbstractValidator<EditSkuInfoCommand>
{
	public EditSkuInfoCommandValidation()
	{
		RuleFor(q => q.InStock).GreaterThanOrEqualTo(0);
		RuleFor(q => q.AddOnPrice).GreaterThanOrEqualTo(0);
	}
}
