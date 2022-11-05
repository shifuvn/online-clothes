namespace OnlineClothes.MailLib.Views.Emails.VerifyAccount;

public class VerifyAccountEmailModel
{
	public VerifyAccountEmailModel(string confirmUrl)
	{
		ConfirmUrl = confirmUrl;
	}

	public string ConfirmUrl { get; set; }
}
