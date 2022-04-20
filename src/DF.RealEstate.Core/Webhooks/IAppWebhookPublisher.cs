using System.Threading.Tasks;
using DF.RealEstate.Authorization.Users;

namespace DF.RealEstate.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
