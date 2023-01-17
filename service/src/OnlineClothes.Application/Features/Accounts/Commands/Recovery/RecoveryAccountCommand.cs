using FluentValidation;

namespace OnlineClothes.Application.Features.Accounts.Commands.Recovery;

public sealed class RecoveryAccountCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Email { get; init; } = null!;
}

internal sealed class RecoveryCommandValidation : AbstractValidator<RecoveryAccountCommand>
{
	public RecoveryCommandValidation()
	{
		RuleFor(q => q.Email)
			.EmailAddress().WithMessage("Email không hợp lệ");
	}
}
