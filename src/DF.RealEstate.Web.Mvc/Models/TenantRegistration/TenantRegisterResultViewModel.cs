using Abp.AutoMapper;
using DF.RealEstate.MultiTenancy.Dto;

namespace DF.RealEstate.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}