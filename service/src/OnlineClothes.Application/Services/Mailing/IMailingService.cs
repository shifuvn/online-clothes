using Microsoft.AspNetCore.Http;
using OnlineClothes.Application.Services.Mailing.Models;

namespace OnlineClothes.Application.Services.Mailing;

public interface IMailingService
{
	Task SendEmailAsync(string to, string subject, string content, string? from = null,
		IList<IFormFile>? attachments = null,
		CancellationToken cancellationToken = default);

	Task SendEmailAsync(MailingTemplate mailing, CancellationToken cancellationToken = default);
}
