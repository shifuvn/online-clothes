using MailKit.Net.Smtp;

namespace OnlineClothes.Application.Services.Mailing;

public interface IMailingProviderConnection
{
	bool IsConnected { get; }

	ISmtpClient SmtpClient();

	bool RetryConnect();
}
