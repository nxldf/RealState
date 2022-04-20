using Abp.AutoMapper;
using DF.RealEstate.Sessions.Dto;

namespace DF.RealEstate.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}