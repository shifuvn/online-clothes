using FluentValidation;

namespace OnlineClothes.Application.Features.Accounts.Queries.Activate;

public sealed class ActivateQuery : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Token { get; init; } = null!;
}

internal sealed class ActivateQueryValidation : AbstractValidator<ActivateQuery>
{
	public ActivateQueryValidation()
	{
		RuleFor(q => q.Token)
			.NotEmpty().WithMessage("Không hợp lệ");
	}
}
