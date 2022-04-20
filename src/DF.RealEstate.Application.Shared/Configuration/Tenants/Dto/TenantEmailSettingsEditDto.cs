using Abp.Auditing;
using DF.RealEstate.Configuration.Dto;

namespace DF.RealEstate.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}