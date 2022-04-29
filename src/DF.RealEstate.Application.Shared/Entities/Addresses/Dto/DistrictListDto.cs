using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Entities.Addresses.Dto
{
    public class DistrictListDto :NullableIdDto
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public virtual CityListDto City { get; set; }
    }
}
