using FluentValidation;
using MediatR;
using OnlineClothes.Domain.Common;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.UpdateInfo;

public class UpdateProductCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string ProductId { get; set; } = null!;

	public UpdateProductCommandJsonBody Body { get; init; } = null!;

	public class UpdateProductCommandJsonBody
	{
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public double Price { get; set; }
		public HashSet<string> Tags { get; set; } = new();
		public HashSet<string> Categories { get; init; } = new();

		public HashSet<ClotheSize> Sizes { get; init; } = new();
		public HashSet<ClotheMaterial> Materials { get; init; } = new();
	}
}

public sealed class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
	public UpdateProductCommandValidation()
	{
		RuleFor(q => q.Body.Name)
			.NotEmpty().WithMessage("Tên sản phẩm không được để trống");
		RuleFor(q => q.Body.Description)
			.NotEmpty().WithMessage("Mô tả không được để trống");
		RuleFor(q => q.Body.Price)
			.GreaterThanOrEqualTo(0).WithMessage("Giá sản phẩm không hợp lệ");
	}
}
