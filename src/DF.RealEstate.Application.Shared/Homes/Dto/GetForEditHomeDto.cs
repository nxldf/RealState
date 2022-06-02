using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using DF.RealEstate.Addresses.Districts.Dto;
using DF.RealEstate.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace DF.RealEstate.Homes.Dto
{
    public class GetForEditHomeDto : NullableIdDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType type { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
        public GetForEditDistrictDto District { get; set; }
        public int DistrictId { get; set; }

        public long Space { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
    }
}
