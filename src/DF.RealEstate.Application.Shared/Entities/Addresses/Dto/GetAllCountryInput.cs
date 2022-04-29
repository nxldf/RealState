using Abp.Runtime.Validation;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Entities.Addresses.Dto
{
    public class GetAllCountryInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name ASC";
            }
        }
    }
}
