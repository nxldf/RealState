using DF.RealEstate.Editions;
using DF.RealEstate.Editions.Dto;
using DF.RealEstate.MultiTenancy.Payments;
using DF.RealEstate.Security;
using DF.RealEstate.MultiTenancy.Payments.Dto;

namespace DF.RealEstate.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
