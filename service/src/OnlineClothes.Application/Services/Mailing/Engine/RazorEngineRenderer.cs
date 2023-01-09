using System.Collections.Concurrent;
using OnlineClothes.Support.Exceptions;
using RazorEngineCore;

namespace OnlineClothes.Application.Services.Mailing.Engine;

public class RazorEngineRenderer
{
	private const string RootDirectoryContainTemplate = @"./Views/MailTemplates/";

	private readonly ILogger<RazorEngineRenderer> _logger;

	private readonly ConcurrentDictionary<string, string> _templates = new();

	public RazorEngineRenderer(ILogger<RazorEngineRenderer> logger)
	{
		_logger = logger;
	}

	public string RenderToString(string templateName, object model)
	{
		if (_templates.TryGetValue(templateName, out var rawHtml))
		{
			return CompileTemplate(rawHtml, model);
		}

		var rawLoadedHtml =
			File.ReadAllText(RootDirectoryContainTemplate + templateName);
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

	public void LoadTemplateToMemory()
	{
		var directoryInfo = new DirectoryInfo(RootDirectoryContainTemplate);
		var files = directoryInfo.GetFiles();

		_logger.LogInformation("Loading mail templates from @\"{Directory}\"", directoryInfo.FullName);


		foreach (var fileInfo in files)
		{
			var rawContent = File.ReadAllText(fileInfo.FullName);
			_templates.TryAdd(fileInfo.Name, rawContent);
		}

		_logger.LogInformation("Loaded mail templates [{Names}]", _templates.Keys);
	}
}
