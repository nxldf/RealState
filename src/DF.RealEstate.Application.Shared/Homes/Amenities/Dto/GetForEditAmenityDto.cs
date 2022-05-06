using Abp.Application.Services.Dto;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Amenities.Dto
{
    public class GetForEditAmenityDto: NullableIdDto, ILocalizedDto<AmenityTranslationDto>
    {
        public string AdminName { get; set; }
        public IList<AmenityTranslationDto> Translations { get; set; }
    }
}
