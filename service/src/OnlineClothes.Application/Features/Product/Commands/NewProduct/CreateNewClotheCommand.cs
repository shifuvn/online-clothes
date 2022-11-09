using FluentValidation;
using MediatR;
using OnlineClothes.Domain.Common;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.NewProduct;

public class CreateNewClotheCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Name { get; init; } = null!;
	public string Description { get; init; } = null!;
	public HashSet<string> Tags { get; init; } = new();
	public HashSet<string> Categories { get; init; } = new();
	public double Price { get; init; }
	public uint Stock { get; init; }
	public HashSet<ClotheSize> Sizes { get; init; } = new();
	public HashSet<ClotheMaterial> Materials { get; init; } = new();
	public ClotheType Type { get; set; }
}

internal sealed class CreateNewClotheCommandValidation : AbstractValidator<CreateNewClotheCommand>
{
	public CreateNewClotheCommandValidation()
	{
		RuleFor(q => q.Name)
			.NotEmpty().WithMessage("Tên sản phẩm không được để trống");
		RuleFor(q => q.Description)
			.NotEmpty().WithMessage("Mô tả không được để trống");
		RuleFor(q => q.Price)
			.GreaterThanOrEqualTo(0).WithMessage("Giá sản phẩm không hợp lệ");
	}
}
