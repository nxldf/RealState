using System.Collections.Generic;
using DF.RealEstate.DynamicEntityProperties.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
