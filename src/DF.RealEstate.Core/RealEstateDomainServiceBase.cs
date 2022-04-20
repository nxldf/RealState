using Abp.Domain.Services;

namespace DF.RealEstate
{
    public abstract class RealEstateDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected RealEstateDomainServiceBase()
        {
            LocalizationSourceName = RealEstateConsts.LocalizationSourceName;
        }
    }
}
