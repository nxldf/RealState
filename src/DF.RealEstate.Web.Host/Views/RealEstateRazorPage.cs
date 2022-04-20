using Abp.AspNetCore.Mvc.Views;

namespace DF.RealEstate.Web.Views
{
    public abstract class RealEstateRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected RealEstateRazorPage()
        {
            LocalizationSourceName = RealEstateConsts.LocalizationSourceName;
        }
    }
}
