using System.Threading.Tasks;
using Abp.Webhooks;

namespace DF.RealEstate.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
