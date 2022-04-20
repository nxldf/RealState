using DF.RealEstate.MultiTenancy.Payments;

namespace DF.RealEstate.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}