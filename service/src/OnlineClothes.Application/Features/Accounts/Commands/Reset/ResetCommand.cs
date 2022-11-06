using FluentValidation;
using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.Reset;

public sealed class ResetCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Email { get; init; } = null!;
}

internal sealed class RecoveryCommandValidation : AbstractValidator<ResetCommand>
{
	public RecoveryCommandValidation()
	{
		RuleFor(q => q.Email)
			.EmailAddress().WithMessage("Email không hợp lệ");
	}
}
