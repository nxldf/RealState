using Abp.AutoMapper;
using DF.RealEstate.Homes.Amenities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Models.Amenities
{
    [AutoMapFrom(typeof(GetForEditAmenityDto))]
    public class AmenityTabViewModel : GetForEditAmenityDto
    {
        public bool IsEditMode => Id.HasValue;
        public bool Selected { get; set; }

    }
}
