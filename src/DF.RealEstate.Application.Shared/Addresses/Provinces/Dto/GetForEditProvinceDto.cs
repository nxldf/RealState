using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Countries.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Addresses.Provinces.Dto
{
    public class GetForEditProvinceDto : NullableIdDto
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual CountryListDto Country { get; set; }
    }
}
