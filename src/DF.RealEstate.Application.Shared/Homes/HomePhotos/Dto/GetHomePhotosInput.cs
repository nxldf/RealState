using Abp.Runtime.Validation;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.HomePhotos.Dto
{
    public class GetHomePhotosInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public long HomeId { get; set; }
        public void Normalize()
        {
            Sorting = "CreationTime DESC";
        }
    }
}
