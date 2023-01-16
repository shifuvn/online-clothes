using FluentValidation;

namespace OnlineClothes.Application.Features.Products.Commands.ImportSku;

public class ImportSkuStockCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Sku { get; set; } = null!;
	public int Quantity { get; set; }
}

public class ImportProductsCommandValidation : AbstractValidator<ImportSkuStockCommand>
{
	public ImportProductsCommandValidation()
	{
		RuleFor(q => q.Sku)
			.NotEmpty().WithMessage("Mã sản phẩm không được trống");
		RuleFor(q => q.Quantity)
			.GreaterThanOrEqualTo(0);
	}
}
