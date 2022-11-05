﻿using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Infrastructure.Services.Mailing.Models;

public class MailingTemplate<TModel>
{
	public MailingTemplate(string to,
		string subject,
		string templateName,
		TModel model,
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
	public TModel Model { get; set; }
	public IList<IFormFile>? AttachmentsFile { get; set; }
	public string? From { get; set; }
}
