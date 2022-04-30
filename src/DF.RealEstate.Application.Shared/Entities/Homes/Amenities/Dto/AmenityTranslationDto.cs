using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Entities.Homes.Amenities.Dto
{
    public class AmenityTranslationDto : IEntityTranslationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
    }
}
