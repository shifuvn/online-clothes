using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace OnlineClothes.MailLib.Service;

internal sealed class RazorRenderer : IRazorRenderer
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ITempDataProvider _tempDataProvider;
	private readonly IRazorViewEngine _viewEngine;

	public RazorRenderer(IServiceProvider serviceProvider, ITempDataProvider tempDataProvider,
		IRazorViewEngine viewEngine)
	{
		_serviceProvider = serviceProvider;
		_tempDataProvider = tempDataProvider;
		_viewEngine = viewEngine;
	}

	public async Task<string> RenderToStringAsync<TModel>(string viewName, TModel model)
	{
		var actionContext = GetActionContext();
		var view = FindView(actionContext, viewName);

		await using var output = new StringWriter();
		var viewContext = new ViewContext(
			actionContext,
			view,
			new ViewDataDictionary<TModel>(
				new EmptyModelMetadataProvider(),
				new ModelStateDictionary())
			{
				Model = model
			},
			new TempDataDictionary(
				actionContext.HttpContext,
				_tempDataProvider),
			output,
			new HtmlHelperOptions());

		await view.RenderAsync(viewContext);

		return output.ToString();
	}

	private IView FindView(ActionContext actionContext, string viewName)
	{
		var getViewResult = _viewEngine.GetView(null, viewName, true);
		if (getViewResult.Success) return getViewResult.View;

		var findViewResult = _viewEngine.FindView(actionContext, viewName, true);
		if (findViewResult.Success) return findViewResult.View;

		var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
		var errorMessage = string.Join(
			Environment.NewLine,
			new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(
				searchedLocations));

		throw new InvalidOperationException(errorMessage);
	}

	private ActionContext GetActionContext()
	{
		var httpContext = new DefaultHttpContext
		{
			RequestServices = _serviceProvider
		};
		return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
	}
}
