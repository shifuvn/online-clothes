namespace OnlineClothes.MailLib.Service;

public interface IRazorRenderer
{
	Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
}
