using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Localization;
using DF.RealEstate.Authorization;
using DF.RealEstate.Entities.Homes.Amenities;
using DF.RealEstate.Entities.Homes.Amenities.Dto;
using DF.RealEstate.Web.Areas.App.Models.Amenities;
using DF.RealEstate.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Controllers.Homes
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration)]
    public class AmenitiesController : RealEstateControllerBase
    {
        private readonly IAmenityAppService _amenityAppService;
        private readonly ILanguageManager _languageManager;

        public AmenitiesController(IAmenityAppService amenityAppService, ILanguageManager languageManager)
        {
            _amenityAppService = amenityAppService;
            _languageManager = languageManager;
        }


        public ActionResult Index()
        {
            var model = new AmenityViewModel
            {
                FilterText = "",

            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditAmenityModal(int? id)
        {
            var output = await _amenityAppService.GetForEdit(new NullableIdDto() { Id = id });
            var model = ObjectMapper.Map<GetForEditAmenityViewModel>(output);

            //langs
            var langs = _languageManager.GetActiveLanguages().ToList();
            foreach (var item in langs)
                if (!model.Translations.Any(x => x.Language == item.Name))
                    model.Translations.Add(new AmenityTranslationDto()
                    {
                        Language = item.Name
                    });

            return PartialView("_CreateOrEditAmenityModal", model);
        }
    }
}
