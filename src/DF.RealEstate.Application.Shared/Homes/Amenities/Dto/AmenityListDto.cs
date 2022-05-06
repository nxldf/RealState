using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Amenities.Dto
{
    public class AmenityListDto : EntityDto
    {
        public string AdminName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
