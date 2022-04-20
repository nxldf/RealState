using Abp.AutoMapper;
using DF.RealEstate.MultiTenancy;
using DF.RealEstate.MultiTenancy.Dto;
using DF.RealEstate.Web.Areas.App.Models.Common;

namespace DF.RealEstate.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}