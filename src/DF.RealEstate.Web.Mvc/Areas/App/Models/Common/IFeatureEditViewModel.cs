using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DF.RealEstate.Editions.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}