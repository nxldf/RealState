using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Provinces.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Addresses.Cities.Dto
{
    public class CityListDto : EntityDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ProvinceId { get; set; }
        public ProvinceListDto Province { get; set; }

    }
}
