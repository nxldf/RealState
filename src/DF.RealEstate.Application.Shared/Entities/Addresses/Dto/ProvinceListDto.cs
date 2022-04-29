using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Entities.Addresses.Dto
{
    public class ProvinceListDto: NullableIdDto
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual CountryListDto Country { get; set; }
    }
}
