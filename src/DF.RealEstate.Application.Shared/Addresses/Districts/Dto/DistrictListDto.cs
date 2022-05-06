using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Cities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Addresses.Districts.Dto
{
    public class DistrictListDto : EntityDto
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public CityListDto City { get; set; }
    }
}
