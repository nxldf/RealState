using Abp.Application.Services.Dto;
using DF.RealEstate.Homes.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Amenities.Dto
{
    public class AmenityListDto : EntityDto
    {
        public string AdminName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public GetForEditHomeDto Homes { get; set; }
        //public long? HomeId
        //{
        //    get
        //    {
        //        if (Homes != null)
        //            return Homes.Id;
        //        return null;
        //    }
        //}

    }
}
