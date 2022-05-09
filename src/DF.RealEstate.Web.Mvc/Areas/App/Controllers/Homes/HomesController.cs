using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Localization;
using DF.RealEstate.Authorization;
using DF.RealEstate.Entities.Addresses;
using DF.RealEstate.Homes;
using DF.RealEstate.Web.Areas.App.Models.Homes;
using DF.RealEstate.Web.Areas.App.Models.Advertisements;
using DF.RealEstate.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DF.RealEstate.Homes.Advertisements;
using Abp.UI;

namespace DF.RealEstate.Web.Areas.App.Controllers.Homes
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration)]
    public class HomesController : RealEstateControllerBase
    {
        private readonly IHomeAppService _homeAppService;
        private readonly IAdvertisementAppService _advertisementAppService;

        public HomesController(IHomeAppService homeAppService,
            IAdvertisementAppService advertisementAppService)
        {
            _homeAppService = homeAppService;
            _advertisementAppService = advertisementAppService;
        }

        public ActionResult Index()
        {
            var model = new HomesViewModel
            {
                FilterText = "",

            };

            return View(model);
        }
        public async Task<ActionResult> Detail(long? id)
        {

            var output = await _homeAppService.GetHomeForEdit(new NullableIdDto<long>() { Id = id });
            var model = ObjectMapper.Map<GetForEditHomeModel>(output);
            return View(model);
        }   
        public  ActionResult AdvertisementTab(long? id)
        {
            if (id.HasValue)
                ViewBag.Id = id;
            return View();
        }

        public ActionResult AmenityTab(long? id)
        {
            if (id.HasValue)
                ViewBag.Id = id;
            return View();
        }


        public PartialViewResult CreateHomeModal()
        {
            return PartialView("_CreateHomeModal");
        }

        public async Task<PartialViewResult> CreateOrEditAdvertisementModal(int? id ,long? homeId )
        {
            if (!id.HasValue && !homeId.HasValue)
                throw new UserFriendlyException("Id And HomeId Are Null ");
            ViewBag.HomeId = homeId;
            var output = await _advertisementAppService.GetForEdit(new NullableIdDto() { Id = id });
            var model = ObjectMapper.Map<GetForEditAdvertisementModel>(output);
            return PartialView("_CreateOrEditAdvertisementModal",model);
        }
    }
}