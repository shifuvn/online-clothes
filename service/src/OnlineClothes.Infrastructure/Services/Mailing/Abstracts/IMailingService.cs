using Microsoft.AspNetCore.Http;
using OnlineClothes.Infrastructure.Services.Mailing.Models;

namespace OnlineClothes.Infrastructure.Services.Mailing.Abstracts;

public interface IMailingService
{
	Task SendEmailAsync(string to, string subject, string content, string? from = null,
		IList<IFormFile>? attachments = null,
		CancellationToken cancellationToken = default);

	Task SendEmailAsync(MailingTemplate mailing, CancellationToken cancellationToken = default);
}
