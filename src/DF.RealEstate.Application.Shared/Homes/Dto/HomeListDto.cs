using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Districts.Dto;
using DF.RealEstate.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Dto
{
    public class HomeListDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
        public virtual DistrictListDto District { get; set; }
        public int DistrictId { get; set; }

        public long Space { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }

        public string FullAddress
        {
            get
            {
                if (District != null)
                    return Address + ", " + (District.Name) + ", " +
                        (District.City.Name) + ", " +
                        (District.City.Province.Name) + ", " +
                        (District.City.Province.Country.Name);
                return null;
            }
        }
    }
}
