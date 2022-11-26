using MailKit.Net.Smtp;

namespace OnlineClothes.Infrastructure.Services.Mailing.Abstracts;

public interface IMailingProviderConnection
{
	bool IsConnected { get; }

	ISmtpClient SmtpClient();

	bool RetryConnect();
}
