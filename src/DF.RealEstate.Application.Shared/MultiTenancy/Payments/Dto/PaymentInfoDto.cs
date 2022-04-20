using DF.RealEstate.Editions.Dto;

namespace DF.RealEstate.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }

        public bool IsLessThanMinimumUpgradePaymentAmount()
        {
            return AdditionalPrice < RealEstateConsts.MinimumUpgradePaymentAmount;
        }
    }
}
