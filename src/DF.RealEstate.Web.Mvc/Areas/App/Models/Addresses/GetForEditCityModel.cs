using Abp.AutoMapper;
using DF.RealEstate.Addresses.Cities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Models.Addresses
{
    [AutoMapFrom(typeof(GetForEditCityDto))]
    public class GetForEditCityModel : GetForEditCityDto
    {
        public bool IsEditMode => Id.HasValue;
    }
}
