using Abp.Application.Services.Dto;
using DF.RealEstate.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.GeHomesInformations
{


    public class HomeInformation : NullableIdDto<long>
    {



        // This fild can be an Enum Type 
        public PropertyType Type { get; set; }
        public int AdvertisementType { get; set; }
        // This fild can be an Enum Type 
        public string Name { get; set; }
        public string Condition { get; set; }
        public string DistrictName { get; set; }
        public int DistrictId { get; set; }
        public long HomeId { get; set; }
        public long Space { get; set; }
        public int Room { get; set; }
        public int TopNumber { get; set; }
        public int FloorSpace { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Toilets { get; set; }
        // This fild can be an Enum Type 
        public string Floors { get; set; }
        // This fild can be an Enum Type 
        public string Heating { get; set; }
        public string Accessible { get; set; }
        public string Limitation { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        //EquipmentAndOpenSpaces
        public string BasementCellar { get; set; }
        public string Loggia { get; set; }
        public string Elevator { get; set; }
        public string StorageRoom { get; set; }
        public string Garage { get; set; }
        public string Balcony { get; set; }
        public string EquippedKitchen { get; set; }
        public string ParkingSpot { get; set; }
        public string Pool { get; set; }
        public string Furnished { get; set; }
        public string PartlyFurnished { get; set; }
        public string AirConditioning { get; set; }
        public string BarrierFree { get; set; }
        public string Carport { get; set; }
               
        public string Garden { get; set; }
        public string Terrace { get; set; }

        //Description
        public string Description { get; set; }

        //PriceAndDetailedInformation
        public decimal MonthlyCosts_Incl { get; set; }
        public decimal Rent_Incl { get; set; }
        public decimal Rent_Excl { get; set; }
        public decimal TotalCharge_Excl { get; set; }
        public decimal OperatingCosts_Excl { get; set; }
        public decimal OperatingCosts_Incl { get; set; }
        public decimal Heating_Excl { get; set; }
        public decimal Heating_Incl { get; set; }
        public decimal OperatingCosts { get; set; }
        public decimal OperatingCostsPercentage { get; set; }
        public decimal TotalLoad { get; set; }
        public decimal TotalRent { get; set; }
        public decimal Deposit { get; set; }


        public string BrokerCommission { get; set; }
        public string AdditionalInformation { get; set; }

        public DateTime? AvailableDate { get; set; } = DateTime.Now;
        public decimal NetPrice { get; set; }
        public bool HideAddress { get; set; } = true;
        public bool HidePreciseLocation { get; set; } = true;
        public bool ContactByPhone { get; set; } = false;
        public bool ContactByEmail { get; set; } = false;
        public bool ContactSiteMessage { get; set; } = false;
    }
    
}

