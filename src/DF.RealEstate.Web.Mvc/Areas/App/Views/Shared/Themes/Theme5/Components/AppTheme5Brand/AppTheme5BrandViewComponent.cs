using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DF.RealEstate.Web.Areas.App.Models.Layout;
using DF.RealEstate.Web.Session;
using DF.RealEstate.Web.Views;

namespace DF.RealEstate.Web.Areas.App.Views.Shared.Themes.Theme5.Components.AppTheme5Brand
{
    public class AppTheme5BrandViewComponent : RealEstateViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme5BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
