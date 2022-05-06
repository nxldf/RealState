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

namespace DF.RealEstate.Web.Areas.App.Controllers.Homes
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration)]
    public class HomesController : RealEstateControllerBase
    {
        private readonly IHomeAppService _homeAppService;

        public HomesController(IHomeAppService homeAppService)
        {
            _homeAppService = homeAppService;
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
            //ViewBag.Country = new SelectList(await _addressAppService.GetCountryDropdown(), "Id", "Title");
            //ViewBag.Province = new SelectList(await _addressAppService.GetProvinceDropdown(), "Id", "Title");
            //ViewBag.City = new SelectList(await _addressAppService.GetCityDropdown(), "Id", "Title");
            //ViewBag.District = new SelectList(await _addressAppService.GetDistrictDropdown(), "Id", "Title");

            var output = await _homeAppService.GetHomeForEdit(new NullableIdDto<long>() { Id = id });
            var model = ObjectMapper.Map<GetForEditHomeModel>(output);
            return View(model);
        }   
        public async Task<ActionResult> AdvertisementTab(long? id)
        {
            if (id.HasValue)
                ViewBag.Id = id;
            else
                ViewBag.Id = 1;
            var model = new GetForEditAdvertisementModel();
            return View(model);
        }

        public  PartialViewResult CreateHomeModal()
        {
            return PartialView("_CreateHomeModal");
        }
    }
}
