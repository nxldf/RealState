using Abp.AutoMapper;
using DF.RealEstate.MultiTenancy.Dto;

namespace DF.RealEstate.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
