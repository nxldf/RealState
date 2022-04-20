using System.Collections.Generic;
using DF.RealEstate.Editions.Dto;
using DF.RealEstate.MultiTenancy.Payments;

namespace DF.RealEstate.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}