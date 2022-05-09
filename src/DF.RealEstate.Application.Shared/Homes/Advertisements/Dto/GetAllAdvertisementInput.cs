using Abp.Runtime.Validation;
using DF.RealEstate.Dto;
using DF.RealEstate.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Advertisements.Dto
{
    public class GetAllAdvertisementInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public AdvertisementType? Type { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
