using Abp.AutoMapper;
using DF.RealEstate.Homes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.RealEstate.Web.Areas.App.Models.Homes
{
    [AutoMapFrom(typeof(GetForEditHomeDto))]
    public class GetForEditHomeModel : GetForEditHomeDto
    {
        
    }
}
