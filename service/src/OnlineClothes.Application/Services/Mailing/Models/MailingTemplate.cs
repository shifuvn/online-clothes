using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Services.Mailing.Models;

public class MailingTemplate
{
	public MailingTemplate(string to,
		string subject,
		string templateName,
		object model,
		IList<IFormFile>? attachmentsFile = null,
		string? from = null)
	{
		To = to;
		Subject = subject;
		Model = model;
		TemplateName = templateName;
		From = from;
		AttachmentsFile = attachmentsFile;
	}

	public string To { get; set; }
	public string Subject { get; set; }
	public string TemplateName { get; set; }
	public object Model { get; set; }
	public IList<IFormFile>? AttachmentsFile { get; set; }
	public string? From { get; set; }
}
