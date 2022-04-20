using System.Collections.Generic;
using DF.RealEstate.Editions;
using DF.RealEstate.Editions.Dto;
using DF.RealEstate.MultiTenancy.Payments;
using DF.RealEstate.MultiTenancy.Payments.Dto;

namespace DF.RealEstate.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
