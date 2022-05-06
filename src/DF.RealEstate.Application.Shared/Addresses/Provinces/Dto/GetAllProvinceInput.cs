using Abp.Runtime.Validation;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Addresses.Provinces.Dto
{
    public class GetAllProvinceInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public int? CountryId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
