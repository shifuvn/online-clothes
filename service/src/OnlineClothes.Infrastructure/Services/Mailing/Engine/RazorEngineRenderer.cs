using System.Collections.Concurrent;
using OnlineClothes.Support.Exceptions;
using RazorEngineCore;

namespace OnlineClothes.Infrastructure.Services.Mailing.Engine;

public class RazorEngineRenderer
{
	private readonly ConcurrentDictionary<string, string> _templates = new();

	public string RenderToString(string templateName, object model)
	{
		if (_templates.TryGetValue(templateName, out var rawHtml))
		{
			return CompileTemplate(rawHtml, model);
		}

		var rawLoadedHtml =
			File.ReadAllText(@"../OnlineClothes.Infrastructure/Services/Mailing/Templates/" + templateName);
		NullValueReferenceException.ThrowIfNull(rawLoadedHtml);

		_templates.TryAdd(templateName, rawLoadedHtml);
		return CompileTemplate(rawLoadedHtml, model);
	}

	private static string CompileTemplate(string raw, object model)
	{
		var razorEngine = new RazorEngine();
		var compiledTemplate = razorEngine.Compile(raw);
		return compiledTemplate.Run(model);
	}
}
