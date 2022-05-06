using Abp.Application.Services.Dto;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Amenities.Dto
{
    public class CreateOrEditAmenityDto : NullableIdDto, ILocalizedDto<AmenityTranslationDto>
    {
        public IList<AmenityTranslationDto> Translations { get; set; }
    }
}
