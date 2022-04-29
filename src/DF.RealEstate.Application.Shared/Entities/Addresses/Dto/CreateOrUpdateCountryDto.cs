using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Entities.Addresses.Dto
{
    public class CreateOrUpdateCountryDto : NullableIdDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public string Flag { get; set; }
    }
}
