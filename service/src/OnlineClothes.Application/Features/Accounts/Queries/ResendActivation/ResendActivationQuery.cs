using FluentValidation;

namespace OnlineClothes.Application.Features.Accounts.Queries.ResendActivation;

public class ResendActivationQuery : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Email { get; init; } = null!;
}

internal sealed class ResendActivationCommandValidation : AbstractValidator<ResendActivationQuery>
{
	public ResendActivationCommandValidation()
	{
		RuleFor(q => q.Email)
			.EmailAddress().WithMessage("Email không hợp lệ");
	}
}
