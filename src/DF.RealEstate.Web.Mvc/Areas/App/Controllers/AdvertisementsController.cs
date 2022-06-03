using Abp.AspNetCore.Mvc.Authorization;
using DF.RealEstate.Authorization;
using DF.RealEstate.Web.Areas.App.Models.Advertisements;
using DF.RealEstate.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration)]
    public class AdvertisementsController : RealEstateControllerBase
    {
        public ActionResult Index()
        {
            var model = new AdvertisementViewModel
            {
                FilterText = "",
            };

            return View(model);
        }
    }
}
