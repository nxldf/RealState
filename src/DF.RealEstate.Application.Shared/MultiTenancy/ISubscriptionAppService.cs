using System.Threading.Tasks;
using Abp.Application.Services;

namespace DF.RealEstate.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
