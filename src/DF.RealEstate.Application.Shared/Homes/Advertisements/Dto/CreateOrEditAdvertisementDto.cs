using Abp.Application.Services.Dto;
using DF.RealEstate.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.Advertisements.Dto
{
    public class CreateOrEditAdvertisementDto: NullableIdDto
    {
        public DateTime? AvailableDate { get; set; }
        public AdvertisementType Type { get; set; }
        public decimal NetPrice { get; set; }
        public bool HideAddress { get; set; }
        public bool HidePreciseLocation { get; set; }
        public bool ContactByPhone { get; set; }
        public bool ContactByEmail { get; set; }
        public bool ContactSiteMessage { get; set; }
    }
}
