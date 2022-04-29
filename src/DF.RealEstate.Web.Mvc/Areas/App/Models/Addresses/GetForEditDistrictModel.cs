using Abp.AutoMapper;
using DF.RealEstate.Entities.Addresses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Models.Addresses
{
    [AutoMapFrom(typeof(GetForEditDistrictDto))]
    public class GetForEditDistrictModel : GetForEditDistrictDto
    {
        public bool IsEditMode => Id.HasValue;
    }
}
