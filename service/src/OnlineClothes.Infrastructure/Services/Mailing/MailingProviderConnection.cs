using System.Net.Sockets;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Application.Services.Mailing.Models;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace OnlineClothes.Infrastructure.Services.Mailing;

public class MailingProviderConnection : IMailingProviderConnection
{
	private readonly object _locker = new();

	private readonly ILogger<MailingProviderConnection> _logger;
	private readonly MailingProviderConfiguration _mailingConfiguration;

	private ISmtpClient? _smtpClient;

	public MailingProviderConnection(ILogger<MailingProviderConnection> logger,
		IOptions<MailingProviderConfiguration> mailingConfigurationOption)
	{
		_logger = logger;
		_mailingConfiguration = mailingConfigurationOption.Value;
	}

	public bool IsConnected => CheckConnection();

	public bool RetryConnect()
	{
		_logger.LogWarning("Attempt to connect to smtp server");

		lock (_locker)
		{
			var policy = Policy.Handle<SocketException>()
				.Or<Exception>()
				.WaitAndRetry(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5));


			policy.Execute(InitiateSmtpConnection);
		}

		_logger.LogInformation("Re-connect to mail service success");

		return true;
	}

	public ISmtpClient SmtpClient()
	{
		if (!IsConnected)
		{
			_logger.LogWarning("SmtpClient is null, trying to reconnect");

			if (RetryConnect())
			{
				return _smtpClient!;
			}

			_logger.LogCritical("Can't connect to smtp server");
			throw new Exception();
		}

		return _smtpClient!;
	}

	private void InitiateSmtpConnection()
	{
		_smtpClient = new SmtpClient();
		var secureSocketOpts = _mailingConfiguration.StartTls
			? SecureSocketOptions.StartTls
			: SecureSocketOptions.None;

		_smtpClient.Connect(_mailingConfiguration.Host, _mailingConfiguration.Port, secureSocketOpts);

		if (secureSocketOpts == SecureSocketOptions.None && _mailingConfiguration.LocalEnv)
		{
			return; // skip authenticate if in local development mode
		}

		_smtpClient.Authenticate(_mailingConfiguration.Username, _mailingConfiguration.Password);
	}

	private bool CheckConnection()
	{
		return _smtpClient is not null && _smtpClient.IsConnected &&
		       (_mailingConfiguration.LocalEnv || _smtpClient!.IsAuthenticated);
	}
}
