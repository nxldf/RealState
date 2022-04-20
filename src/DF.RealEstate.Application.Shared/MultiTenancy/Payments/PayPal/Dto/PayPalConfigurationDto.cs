using System.Collections.Generic;

namespace DF.RealEstate.MultiTenancy.Payments.PayPal.Dto
{
    public class PayPalConfigurationDto
    {
        public string ClientId { get; set; }

        public string DemoUsername { get; set; }

        public string DemoPassword { get; set; }
        
        public List<string> DisabledFundings { get; set; }
    }
}
