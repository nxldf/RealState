using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Addresses.Countries.Dto
{
    public class GetForEditCountryDto : NullableIdDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public string Flag { get; set; }
    }
}
