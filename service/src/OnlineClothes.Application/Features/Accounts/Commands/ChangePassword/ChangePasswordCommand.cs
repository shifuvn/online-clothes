using FluentValidation;

namespace OnlineClothes.Application.Features.Accounts.Commands.ChangePassword;

public sealed class ChangePasswordCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string CurrentPassword { get; init; } = null!;
	public string NewPassword { get; init; } = null!;
}

internal sealed class ChangePasswordCommandValidation : AbstractValidator<ChangePasswordCommand>
{
	public ChangePasswordCommandValidation()
	{
		RuleFor(q => q.CurrentPassword)
			.NotEmpty().WithMessage("Bạn phải nhập mật khẩi cũ");

		RuleFor(q => q.NewPassword)
			.MinimumLength(6).WithMessage("Mật khẩu phải có hơn 6 ký tự");
	}
}
