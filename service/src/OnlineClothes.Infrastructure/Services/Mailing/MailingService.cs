using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Application.Services.Mailing.Engine;
using OnlineClothes.Application.Services.Mailing.Models;

namespace OnlineClothes.Infrastructure.Services.Mailing;

internal sealed class MailingService : IMailingService
{
	private readonly ILogger<MailingService> _logger;
	private readonly MailingProviderConfiguration _mailingConfiguration;
	private readonly IMailingProviderConnection _mailingProvider;
	private readonly RazorEngineRenderer _razorEngineRenderer;

	public MailingService(ILogger<MailingService> logger,
		IOptions<MailingProviderConfiguration> mailingConfigurationOption,
		IMailingProviderConnection mailingProvider, RazorEngineRenderer razorEngineRenderer)
	{
		_logger = logger;
		_mailingConfiguration = mailingConfigurationOption.Value;
		_mailingProvider = mailingProvider;
		_razorEngineRenderer = razorEngineRenderer;
	}

	public async Task SendEmailAsync(string to, string subject, string content, string? from = null,
		IList<IFormFile>? attachments = null,
		CancellationToken cancellationToken = default)
	{
		var email = new MimeMessage();
		email.From.Add(MailboxAddress.Parse(string.IsNullOrEmpty(from) ? _mailingConfiguration.EmailFrom : from));
		email.To.Add(MailboxAddress.Parse(to));
		email.Subject = subject;

		// build htmlBody
		var bodyBuilder = new BodyBuilder();

		// attachments
		if (attachments is not null)
		{
			foreach (var file in attachments)
			{
				if (file.Length <= 0)
				{
					continue;
				}

				using var ms = new MemoryStream();
				await file.CopyToAsync(ms, cancellationToken);
				var fileBytes = ms.ToArray();
				bodyBuilder.Attachments.Add(file.Name, fileBytes, ContentType.Parse(file.ContentType));
			}
		}

		// build html
		bodyBuilder.HtmlBody = content;
		email.Body = bodyBuilder.ToMessageBody();

		if (!_mailingProvider.IsConnected)
		{
			_mailingProvider.RetryConnect();
		}

		try
		{
			await _mailingProvider.SmtpClient().SendAsync(email, cancellationToken);
		}
		catch (Exception e)
		{
			_logger.LogCritical(e, "Cannot send email\n {Message}", e.Message);

			_mailingProvider.RetryConnect();

			throw;
		}
	}

	public async Task SendEmailAsync(MailingTemplate mailing,
		CancellationToken cancellationToken = default)
	{
		var bodyContent = _razorEngineRenderer.RenderToString(mailing.TemplateName, mailing.Model);

		await SendEmailAsync(mailing.To,
			mailing.Subject,
			bodyContent,
			mailing.From,
			mailing.AttachmentsFile,
			cancellationToken);
	}
}
