using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.UI;
using DF.RealEstate.Addresses.Cities;
using DF.RealEstate.Addresses.Countries;
using DF.RealEstate.Addresses.Districts;
using DF.RealEstate.Addresses.Provinces;
using DF.RealEstate.Authorization;
using DF.RealEstate.Entities.Addresses;
using DF.RealEstate.Web.Areas.App.Models.Addresses;
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
    public class AddressesController : RealEstateControllerBase
    {
        private readonly ICountryAppService _countryAppService;
        private readonly IProvinceAppService _provinceAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IDistrictAppService _districtAppService;

        public AddressesController(ICountryAppService countryAppService,
            IProvinceAppService provinceAppService,
            ICityAppService cityAppService,
            IDistrictAppService districtAppService)
        {
            _countryAppService = countryAppService;
            _provinceAppService = provinceAppService;
            _cityAppService = cityAppService;
            _districtAppService = districtAppService;
        }

        public ActionResult Index()
        {
            var model = new AddressViewModel
            {
                FilterText = "",
            };

            return View(model);
        }


        public ActionResult Province(int? countryId)
        {
            if (!countryId.HasValue)
                throw new UserFriendlyException("CountryId Is Null ");

            var model = new AddressViewModel
            {
                FilterText = "",
            };

            ViewBag.CountryId = countryId;
            return View(model);
        }

        public ActionResult City(int? countryId, int? provinceId)
        {
            if (!provinceId.HasValue)
                throw new UserFriendlyException("ProvinceId Is Null ");

            var model = new AddressViewModel
            {
                FilterText = "",
            };

            ViewBag.ProvinceId = provinceId;
            ViewBag.CountryId = countryId;
            return View(model);
        }

        public ActionResult District(int? countryId, int? provinceId, int? cityId)
        {
            if (!provinceId.HasValue)
                throw new UserFriendlyException("ProvinceId Is Null ");

            var model = new AddressViewModel
            {
                FilterText = "",
            };

            ViewBag.ProvinceId = provinceId;
            ViewBag.CountryId = countryId;
            ViewBag.CityId = cityId;
            return View(model);
        }


        public async Task<PartialViewResult> CreateOrEditCountryModal(int? id)
        {

            var output = await _countryAppService.GetCountryForEdit(new NullableIdDto() { Id = id });
            var model = ObjectMapper.Map<GetForEditCountryModel>(output);
            return PartialView("_CreateOrEditCountryModal", model);
        }

        public async Task<PartialViewResult> CreateOrEditProvinceModal(int? id, int? countryId)
        {
            if (!id.HasValue && !countryId.HasValue)
                throw new UserFriendlyException("Id And CountryId Are Null ");
            ViewBag.CountryId = countryId;
            var output = await _provinceAppService.GetProvinceForEdit(new NullableIdDto() { Id = id });
            var model = ObjectMapper.Map<GetForEditProvinceModel>(output);
            return PartialView("_CreateOrEditProvinceModal", model);
        }

        public async Task<PartialViewResult> CreateOrEditCityModal(int? id, int? provinceId)
        {
            if (!id.HasValue && !provinceId.HasValue)
                throw new UserFriendlyException("Id And ProvinceId Are Null ");
            ViewBag.ProvinceId = provinceId;
            var output = await _cityAppService.GetCityForEdit(new NullableIdDto() { Id = id });
            var model = ObjectMapper.Map<GetForEditCityModel>(output);
            return PartialView("_CreateOrEditCityModal", model);
        }

        public async Task<PartialViewResult> CreateOrEditDistrictModal(int? id, int? cityId)
        {
            if (!id.HasValue && !cityId.HasValue)
                throw new UserFriendlyException("Id And CityId Are Null ");
            ViewBag.CityId = cityId;
            var output = await _districtAppService.GetDistrictForEdit(new NullableIdDto() { Id = id });
            var model = ObjectMapper.Map<GetForEditDistrictModel>(output);
            return PartialView("_CreateOrEditDistrictModal", model);
        }



    }
}
