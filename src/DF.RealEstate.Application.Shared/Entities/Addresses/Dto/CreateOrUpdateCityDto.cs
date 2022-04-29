using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Entities.Addresses.Dto
{
    public class CreateOrUpdateCityDto : NullableIdDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ProvinceId { get; set; }
        public virtual ProvinceListDto Province { get; set; }
    }
}
