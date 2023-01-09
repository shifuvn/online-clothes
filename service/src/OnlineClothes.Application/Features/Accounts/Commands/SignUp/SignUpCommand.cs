using FluentValidation;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

public sealed class SignUpCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Email { get; init; } = null!;
	public string Password { get; init; } = null!;
	public string ConfirmPassword { get; init; } = null!;
	public string FirstName { get; init; } = null!;
	public string LastName { get; init; } = null!;
}

public sealed class SignUpCommandValidation : AbstractValidator<SignUpCommand>
{
	public SignUpCommandValidation()
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
