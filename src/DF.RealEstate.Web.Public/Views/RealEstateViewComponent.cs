using Abp.AspNetCore.Mvc.ViewComponents;

namespace DF.RealEstate.Web.Public.Views
{
    public abstract class RealEstateViewComponent : AbpViewComponent
    {
        protected RealEstateViewComponent()
        {
            LocalizationSourceName = RealEstateConsts.LocalizationSourceName;
        }
    }
}