using FluentValidation;
using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.ImportProducts;

public class ImportProductStockCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string ProductId { get; set; } = null!;
	public uint Quantity { get; set; }
}

public class ImportProductsCommandValidation : AbstractValidator<ImportProductStockCommand>
{
	public ImportProductsCommandValidation()
	{
		RuleFor(q => q.ProductId)
			.NotEmpty().WithMessage("Mã sản phẩm không được trống");
	}
}
