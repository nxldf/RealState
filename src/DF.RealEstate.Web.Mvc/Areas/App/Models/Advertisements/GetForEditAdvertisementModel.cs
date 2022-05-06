using Abp.AutoMapper;
using DF.RealEstate.Homes.Advertisements.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Models.Advertisements
{
    [AutoMapFrom(typeof(GetForEditAdvertisementDto))]
    public class GetForEditAdvertisementModel: GetForEditAdvertisementDto
    {
    }
}
