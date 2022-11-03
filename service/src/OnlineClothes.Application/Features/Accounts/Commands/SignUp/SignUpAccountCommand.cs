using FluentValidation;
using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

public sealed class SignUpAccountCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Email { get; init; } = null!;
	public string Password { get; init; } = null!;
	public string ConfirmPassword { get; init; } = null!;
}

public sealed class SignUpAccountCommandValidation : AbstractValidator<SignUpAccountCommand>
{
	public SignUpAccountCommandValidation()
	{
		RuleFor(q => q.Email)
			.EmailAddress().WithMessage("Email is invalid")
			.NotEmpty().WithMessage("Email is required");

		RuleFor(q => q.Password)
			.MinimumLength(6).WithMessage("Password should have more than 6 characters");

		RuleFor(q => q.Password)
			.Equal(q => q.ConfirmPassword).WithMessage("Password doesn't match");
	}
}
