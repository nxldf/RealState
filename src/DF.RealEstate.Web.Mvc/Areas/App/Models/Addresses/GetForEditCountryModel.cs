using Abp.AutoMapper;
using DF.RealEstate.Addresses.Countries.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Models.Addresses
{
    [AutoMapFrom(typeof(GetForEditCountryDto))]
    public class GetForEditCountryModel : GetForEditCountryDto
    {
        public bool IsEditMode => Id.HasValue;
    }
}
