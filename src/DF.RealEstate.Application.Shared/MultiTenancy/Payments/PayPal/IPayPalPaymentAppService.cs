using System.Threading.Tasks;
using Abp.Application.Services;
using DF.RealEstate.MultiTenancy.Payments.PayPal.Dto;

namespace DF.RealEstate.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
