using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Amenities.Dto
{
    public class AddOrEditAmenitiesDto : NullableIdDto<long>
    {
        public int[] Amenities { get; set; }
    }
}
