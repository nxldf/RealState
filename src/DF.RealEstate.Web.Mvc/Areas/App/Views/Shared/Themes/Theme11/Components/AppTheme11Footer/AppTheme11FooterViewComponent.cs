﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DF.RealEstate.Web.Areas.App.Models.Layout;
using DF.RealEstate.Web.Session;
using DF.RealEstate.Web.Views;

namespace DF.RealEstate.Web.Areas.App.Views.Shared.Themes.Theme11.Components.AppTheme11Footer
{
    public class AppTheme11FooterViewComponent : RealEstateViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme11FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
