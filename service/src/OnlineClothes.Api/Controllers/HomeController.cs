using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Application.Services.Mailing.Models;
using OnlineClothes.BuildIn.Builders.Predicate;
using OnlineClothes.BuildIn.Exceptions;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Api.Controllers;

public class HomeController : Controller
{
	private readonly IAccountRepository _accountRepository;
	private readonly ILogger<HomeController> _logger;
	private readonly IMailingService _mailingService;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;


	// GET
	public HomeController(
		IAccountRepository accountRepository,
		ITokenRepository tokenRepository,
		IUnitOfWork unitOfWork,
		ILogger<HomeController> logger,
		IMailingService mailingService)
	{
		_accountRepository = accountRepository;
		_tokenRepository = tokenRepository;
		_unitOfWork = unitOfWork;
		_logger = logger;
		_mailingService = mailingService;
	}

	[HttpGet("verified-email")]
	public async Task<IActionResult> VerifiedEmail([FromQuery] string token)
	{
		var tokenCode = await _tokenRepository.FindOneAsync(FilterBuilder<AccountTokenCode>.Where(x =>
			x.TokenCode == token && x.TokenType == AccountTokenType.Verification));
		NullValueReferenceException.ThrowIfNull(tokenCode, nameof(tokenCode));

		if (!tokenCode.IsValid())
		{
			return BadRequest();
		}

		var account =
			await _accountRepository.GetByEmail(tokenCode.Email);

		NullValueReferenceException.ThrowIfNull(account);

		// Begin tx
		await _unitOfWork.BeginTransactionAsync();

		account.Activate();
		_accountRepository.Update(account);

		var updateResult = await _unitOfWork.SaveChangesAsync();

		if (updateResult)
		{
			_tokenRepository.Delete(tokenCode);
			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation("Account {Email} is activated", tokenCode.Email);
		}

		await _unitOfWork.CommitAsync();


		return View("VerifiedEmailWelcome");
	}

	[HttpGet("auth/reset-password")]
	public async Task<IActionResult> ResetPassword([FromQuery] string token)
	{
		var tokenCode = await _tokenRepository.FindOneAsync(
			FilterBuilder<AccountTokenCode>.Where(code =>
				code.TokenCode == token && code.TokenType == AccountTokenType.ResetPassword));

		NullValueReferenceException.ThrowIfNull(tokenCode, nameof(tokenCode));

		if (!tokenCode.IsValid())
		{
			return BadRequest();
		}

		var account = await _accountRepository.GetByEmail(tokenCode.Email);
		var newPassword = PasswordHasher.RandomPassword(8);
		account!.SetPassword(newPassword);

		_accountRepository.Update(account);

		var updatedResult = await _unitOfWork.SaveChangesAsync();

		if (!updatedResult)
		{
			return BadRequest();
		}

		await SendResetPasswordMail(account, newPassword);

		return View("RestPassword");
	}

	private async Task SendResetPasswordMail(AccountUser account, string newPw,
		CancellationToken cancellationToken = default)
	{
		var mail = new MailingTemplate(account.Email, "Reset password", EmailTemplateNames.ResetPassword,
			new
			{
				NewPassword = newPw
			});

		await _mailingService.SendEmailAsync(mail, cancellationToken);
	}
}
