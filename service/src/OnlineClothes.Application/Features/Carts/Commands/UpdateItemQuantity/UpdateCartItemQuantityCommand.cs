using FluentValidation;

namespace OnlineClothes.Application.Features.Carts.Commands.UpdateItemQuantity;

public class UpdateCartItemQuantityCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public UpdateCartItemQuantityCommand(string sku, int number)
	{
		Sku = sku;
		Number = number;
	}

	public UpdateCartItemQuantityCommand()
	{
	}

	public string Sku { get; set; } = null!;
	public int Number { get; set; }
}

public class UpdateCartItemQuantityCommandValidation : AbstractValidator<UpdateCartItemQuantityCommand>
{
	public UpdateCartItemQuantityCommandValidation()
	{
		RuleFor(q => q.Number)
			.GreaterThanOrEqualTo(0).WithMessage("Số lượng phải lơn hơn 0");
	}
}
