using OnlineClothes.Infrastructure.Services.Mailing.Models;

namespace OnlineClothes.Infrastructure.BackgroundService;

public interface IMailingBackgroundTaskQueue
{
	void EnqueueTask<TModel>(MailingTemplate<TModel> mailTemplate);
}
