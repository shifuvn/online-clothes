using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing.Models;
using OnlineClothes.Infrastructure.Services.Mailing.Templates;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

internal sealed class
	SignUpAccountCommandHandler : IRequestHandler<SignUpAccountCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<SignUpAccountCommandHandler> _logger;
	private readonly IMailingService _mailingService;
	private readonly IUserAccountRepository _userAccountRepository;

	public SignUpAccountCommandHandler(ILogger<SignUpAccountCommandHandler> logger,
		IUserAccountRepository userAccountRepository,
		IMailingService mailingService)
	{
		_logger = logger;
		_userAccountRepository = userAccountRepository;
		_mailingService = mailingService;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SignUpAccountCommand request,
		CancellationToken cancellationToken)
	{
		var existingAccount =
			await _userAccountRepository.FindOneAsync(FilterBuilder<UserAccount>.Where(p => p.Email == request.Email),
				cancellationToken);

		if (existingAccount is not null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Tài khoảng đã tồn tại");
		}

		var newAccount = UserAccount.Create(request.Email, request.Password, UserAccountRole.Client);

		var mail = new MailingTemplate(newAccount.Email, "Verify Account", EmailTemplateNames.VerifyAccount,
			new
			{
				ConfirmedUrl = "test.com"
			});

		await _mailingService.SendEmailAsync(mail, cancellationToken);

		await _userAccountRepository.InsertAsync(newAccount, cancellationToken);
		_logger.LogInformation("Create new account {Email}", newAccount.Email);

		return JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created);
	}
}
