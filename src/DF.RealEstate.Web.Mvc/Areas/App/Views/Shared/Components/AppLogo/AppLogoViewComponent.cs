using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DF.RealEstate.Web.Areas.App.Models.Layout;
using DF.RealEstate.Web.Session;
using DF.RealEstate.Web.Views;

namespace DF.RealEstate.Web.Areas.App.Views.Shared.Components.AppLogo
{
    public class AppLogoViewComponent : RealEstateViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppLogoViewComponent(
            IPerRequestSessionCache sessionCache
        )
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync(string logoSkin = null, string logoClass = "")
        {
            var headerModel = new LogoViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                LogoSkinOverride = logoSkin,
                LogoClassOverride = logoClass
            };

            return View(headerModel);
        }
    }
}
